using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Infrastructure.Commands;
using Infrastructure.FileHandler;
using MediatR;

namespace Infrastructure.Handlers
{
    public class UpdateCoverImageHandler : IRequestHandler<UpdateCoverImageCommand, string>
    {
        private readonly IMangaWriteRepo _repo;
        private readonly IFileHandler _handler;

        public UpdateCoverImageHandler(IMangaWriteRepo repo, IFileHandler handler)
        {
            this._repo = repo;
            this._handler = handler;
        }
        public async Task<string> Handle(UpdateCoverImageCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);
            string coverPictureLocation = await _handler.CreateCoverPicturePath(request.MangaId, extension);

            await _repo.UpdateCoverPictureLocation(coverPictureLocation, request.MangaId, cancellationToken);

            _handler.SaveFileToLocation(request.File, coverPictureLocation);

            coverPictureLocation = RemoveRootFolder(coverPictureLocation);

            return coverPictureLocation;
        }

        private string RemoveRootFolder(string path)
        {
            path = path.Replace("wwwroot", "");
            return path.Substring(1);
        }
    }
}
