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
    public class MangaImageUploadHandler : IRequestHandler<MangaInfoUploadCommand, string>
    {
        private readonly IMangaWriteRepo _repo;

        public MangaImageUploadHandler(IMangaWriteRepo repo)
        {
            this._repo = repo;
        }
        public async Task<string> Handle(MangaInfoUploadCommand request, CancellationToken cancellationToken)
        {
            MangaAdditionModel info = new MangaAdditionModel
            {
                Desription = request.MangaDescription,
                MangaTitle = request.MangaTitle
            };

            string id = await _repo.SaveMangaReturnId(info, cancellationToken);

            return id;
        }
    }
}
