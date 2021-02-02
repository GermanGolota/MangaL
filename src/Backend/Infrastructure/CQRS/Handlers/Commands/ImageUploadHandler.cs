using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Commands;
using Infrastructure.Configuration;
using Infrastructure.FileHandler;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Handlers
{
    public class ImageUploadHandler : IRequestHandler<ImageUploadCommand, string>
    {
        private readonly IMangaWriteRepo _mangaRepo;
        private readonly IImageRepo _imageRepo;
        private readonly IFileHandler _fileHandler;

        public ImageUploadHandler(IMangaWriteRepo repo,  IImageRepo imageRepo, 
            IFileHandler fileHandler)
        {
            this._mangaRepo = repo;
            this._imageRepo = imageRepo;
            this._fileHandler = fileHandler;
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

            string imageId = await _mangaRepo.SavePictureReturnId(picture, cancellationToken);

            string fileName = file.FileName;

            string path = await _fileHandler.CreateImagePath(fileName, chapterId, imageId);

            _fileHandler.SaveFileToLocation(file, path);

            path = RemoveRootFolder(path);

            await _imageRepo.UpdatePictureLocation(imageId, path, cancellationToken);

            return imageId;
        }

        private string RemoveRootFolder(string path)
        {
            path = path.Replace("wwwroot", "");
            return path.Substring(1);
        }
    }
}
