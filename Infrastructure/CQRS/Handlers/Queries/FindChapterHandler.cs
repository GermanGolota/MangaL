using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Configuration;
using Infrastructure.FileHandler;
using Infrastructure.Queries;
using MediatR;

namespace Infrastructure.Handlers
{
    public class FindChapterHandler : IRequestHandler<FindChapterQuerie, ChapterModel>
    {
        private readonly IChapterRepo _repo;
        private readonly IFileHandler _fileHandler;

        public FindChapterHandler(IChapterRepo repo, IFileHandler fileHandler)
        {
            this._repo = repo;
            this._fileHandler = fileHandler;
        }
        public async Task<ChapterModel> Handle(FindChapterQuerie request, CancellationToken cancellationToken)
        {
            ChapterModel output = await _repo.GetChapterBy(request.ChapterId, cancellationToken);
            foreach (var picture in output.Pictures)
            {
                picture.ImageLocation = _fileHandler.CreateFullUrlFromStored(picture.ImageLocation);
            }
            return output;
        }
    }
}
