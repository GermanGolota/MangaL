using DataAccess.DTOs;
using Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI
{
    public static class Seeder
    {
        public static async Task SeedUsers(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var mediator = provider.GetRequiredService<IMediator>();

                string userFilePath = Path.Combine(GetSeeededDataDirectory(), "Users.json");

                using (FileStream file = new FileStream(userFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string content = reader.ReadToEnd();
                        List<UserRegistrationModel> users =
                            JsonConvert.DeserializeObject<List<UserRegistrationModel>>(content);
                        foreach (var user in users)
                        {
                            var command = new RegisterUserCommand(user.Username, user.Password);
                            await mediator.Send(command);
                        }
                    }
                }
            }
        }
        private static string GetSeeededDataDirectory()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "SeededData");
        }
        public static async Task SeedMangas(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                IMediator mediator = provider.GetRequiredService<IMediator>();

                string mangaInfoPath = Path.Combine(GetSeeededDataDirectory(), "Manga.json");

                MangaUploadCommand manga =
                      JsonConvert.DeserializeObject<MangaUploadCommand>(GetFileContent(mangaInfoPath));

                string mangaId = await mediator.Send(manga);

                string chaptersInfoPath = Path.Combine(GetSeeededDataDirectory(), "Chapters.json");

                List<ChapterUploadCommand> chapters =
                    JsonConvert.DeserializeObject<List<ChapterUploadCommand>>(GetFileContent(chaptersInfoPath));
                List<string> ChapterIds = new List<string>();
                foreach (var chapter in chapters)
                {
                    chapter.MangaId = mangaId;
                    string id = await mediator.Send(chapter);
                    ChapterIds.Add(id);
                }

                string picturesInfoPath = Path.Combine(GetSeeededDataDirectory(), "Pictures.json");

                List<PictureIntermediary> pictures =
                    JsonConvert.DeserializeObject<List<PictureIntermediary>>(GetFileContent(picturesInfoPath));

                Dictionary<string, string> ChapterNumberToChapterId = new Dictionary<string, string>();

                var picturesWithUniqueChapter = pictures
                        .GroupBy(x => x.Chapter)
                        .Select(x => x.First())
                        .ToList();

                for (int i = 0; i < picturesWithUniqueChapter.Count; i++)
                {
                    ChapterNumberToChapterId.TryAdd(picturesWithUniqueChapter[i].Chapter, ChapterIds[i]);
                }

                string pictureLocationBase = Path.Combine(GetSeeededDataDirectory(), "Pictures");

                Dictionary<PictureIntermediary, IFormFile> pictureToFile =
                    new Dictionary<PictureIntermediary, IFormFile>();

                foreach (var picture in pictures)
                {
                    string path = Path.Combine(pictureLocationBase, picture.Chapter, picture.Order);

                    path = Path.ChangeExtension(path, "jpg");

                    FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

                    IFormFile formFile = new FormFile(file, 0, file.Length,
                        "Alexander", "Oleg");
                    pictureToFile.Add(picture, formFile);
                }

                List<ImageUploadCommand> images = new List<ImageUploadCommand>();

                foreach (var picture in pictures)
                {
                    var form = pictureToFile.GetValueOrDefault(picture);

                    int order = int.Parse(picture.Order);

                    string ChapterId = ChapterNumberToChapterId.GetValueOrDefault(picture.Chapter);

                    images.Add(new ImageUploadCommand(form, ChapterId, order));
                }

                foreach (var image in images)
                {
                    await mediator.Send(image);
                }
            }
        }
        private static string GetFileContent(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private record PictureIntermediary(string Chapter, string Order);
    }
}
