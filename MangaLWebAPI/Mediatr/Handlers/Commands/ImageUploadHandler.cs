using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using MangaLWebAPI.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MangaLWebAPI.Mediatr
{
    public class ImageUploadHandler : IRequestHandler<ImageUploadCommand, string>
    {
        private readonly IMangaWriteRepo _repo;
        private readonly IChapterRepo _chapterRepo;
        private readonly AppConfiguration _config;

        public ImageUploadHandler(IMangaWriteRepo repo, IChapterRepo readRepo, AppConfiguration config)
        {
            this._repo = repo;
            this._chapterRepo = readRepo;
            this._config = config;
        }
        public async Task<string> Handle(ImageUploadCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string chapterId = request.ChapterId;

            var picture = new PictureAdditionModel
            {
                ChapterId = chapterId,
                PictureOrder = request.Order
            };

            string imageId = await _repo.SavePictureReturnId(picture, cancellationToken);

            string mangaId = await _chapterRepo.FindMangaIdForChapter(chapterId, cancellationToken);

            string path = GetFilePath(file, imageId, chapterId, mangaId);

            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            await _repo.UpdatePictureLocation(imageId, path, cancellationToken);

            return imageId;
        }
        private string GetFilePath(IFormFile file, string imageId, string chapterId, string mangaId)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(imageId, fileExtension);

            string rootFolder = _config.GetContentRootPath(); 

            string mangaFolder = Path.Combine(rootFolder, "Mangas", $"{mangaId}");

            string chaptersFolder = Path.Combine(mangaFolder, "Chapters", $"{chapterId}");

            string imageFolderPath = Path.Combine(chaptersFolder, "Images");

            Directory.CreateDirectory(imageFolderPath);

            string filePath = Path.Combine(imageFolderPath, $"{newFileName}");

            return filePath;
        }
    }
}
