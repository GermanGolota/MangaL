using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using MediatR;

namespace MangaLWebAPI.Mediatr.Handlers
{
    public class ChapterUploadHandler : IRequestHandler<ChapterUploadCommand, string>
    {
        private readonly IMangaWriteRepo _repo;

        public ChapterUploadHandler(IMangaWriteRepo repo)
        {
            this._repo = repo;
        }
        public async Task<string> Handle(ChapterUploadCommand request, CancellationToken cancellationToken)
        {
            ChapterAdditionModel info = new ChapterAdditionModel
            {
                ChapterName = request.ChapterName,
                ChapterNumber = request.ChapterNumber,
                MangaId = request.MangaId
            };

            string id = await _repo.SaveChapterReturnId(info, cancellationToken);

            return id;
        }
    }
}
