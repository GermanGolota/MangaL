using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MangaLWebAPI.Mediatr
{
    public class UploadImageHandler : IRequestHandler<UploadImageCommand, string>
    {
        private readonly IMangaWriteRepo _repo;
        private readonly IMangaReadRepo _readRepo;

        public UploadImageHandler(IMangaWriteRepo repo, IMangaReadRepo readRepo)
        {
            this._repo = repo;
            this._readRepo = readRepo;
        }
        public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string chapterId = request.ChapterId;

            if (isNotValidFile(file))
            {
            }

            var picture = new PictureAdditionModel
            {
                ChapterId = chapterId,
                PictureOrder = request.Order
            };

            string imageId = await _repo.SavePictureReturnId(picture, cancellationToken);

            string mangaId = await _readRepo.FindMangaIdForChapter(chapterId, cancellationToken);

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

            string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string mangaFolder = Path.Combine(rootFolder, "Mangas", $"{mangaId}");

            string chaptersFolder = Path.Combine(mangaFolder, "Chapters", $"{chapterId}");

            string imageFolderPath = Path.Combine(chaptersFolder, "Images");

            Directory.CreateDirectory(imageFolderPath);

            string filePath = Path.Combine(imageFolderPath, $"{newFileName}");

            return filePath;
        }
        private bool isNotValidFile(IFormFile file)
        {
            return file == null || file.Length == 0;
        }
    }
}
