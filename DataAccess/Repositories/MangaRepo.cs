using Core.Entities;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MangaRepo : IMangaRepo
    {
        private readonly ISQLClient _client;

        public MangaRepo(ISQLClient client)
        {
            this._client = client;
        }

        public async Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId)
        {
            string sql = @"SELECT MangaTitle, Description FROM Mangas WHERE Id=@MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            List<Manga> matches = await _client.LoadData<Manga, dynamic>
                (sql, parameters, CancellationToken.None);
            if (matches.Count == 0)
            {
                //can't find manga
                return null;
            }
            var firstMatch = matches.First();

            MangaInfoModel output = new MangaInfoModel
            {
                Desription = firstMatch.Description,
                MangaTitle = firstMatch.MangaTitle
            };

            return output;
        }

        public async Task SaveManga(Manga manga)
        {
            await SaveMangaInfo(manga);

            await SaveMangaChapters(manga);

            //should not contain comments by that point
        }

        private async Task SaveMangaInfo(Manga manga)
        {
            string sql = @"INSERT INTO Mangas(Id, Title, Description) VALUES(@Id, @Title, @Description)";

            var parameters = new
            {
                Id = manga.Id,
                Title = manga.MangaTitle,
                Description = manga.Description
            };

            await _client.SaveData<dynamic>(sql, parameters, CancellationToken.None);
        }
        private async Task SaveMangaChapters(Manga manga)
        {
            foreach (var chapter in manga.Chapters)
            {
                await SaveChapterInfo(chapter);

                await SaveChapterPictures(chapter);
            }
        }
        private async Task SaveChapterInfo(Chapter chapter)
        {

            string sql = @"INSERT INTO Chapters(Id, ChapterName, ChapterNumber, MangaId)" +
                            @"VALUES(@Id, @ChapterName, @ChapterNumber, @MangaId)";
            var parameters = new
            {
                Id = chapter.Id,
                ChapterName = chapter.ChapterName,
                ChapterNumber = chapter.ChapterNumber,
                MangaId = chapter.MangaId
            };

            await _client.SaveData<dynamic>(sql, parameters, CancellationToken.None);

        }
        private async Task SaveChapterPictures(Chapter chapter)
        {
            List<Task> insertOperations = new List<Task>();
            foreach (var picture in chapter.Pictures)
            {
                insertOperations.Add(
                    new Task(async () => await InsertPicture(picture))
                );
            }
            await Task.WhenAll(insertOperations);
        }
        private async Task InsertPicture(Picture picture)
        {
            string sql = @"INSERT INTO Pictures(Id, MangaId, PictureOrder, ImageLocation)" +
                            @"VALUES(@Id, @MangaId, @PictureOrder, @ImageLocation)";
            var parameters = new
            {
                Id = picture.Id,
                MangaId = picture.MangaId,
                PicturesOrder = picture.PictureOrder,
                ImageLocation = picture.ImageLocation
            };

            await _client.SaveData<dynamic>(sql, parameters, CancellationToken.None);
        }
    }
}
