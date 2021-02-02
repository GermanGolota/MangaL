using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Commands;
using MediatR;


namespace Infrastructure.Handlers
{
    public class MangaUploadHandler : IRequestHandler<MangaUploadCommand, string>
    {
        private readonly IMangaWriteRepo _repo;

        public MangaUploadHandler(IMangaWriteRepo repo)
        {
            this._repo = repo;
        }
        public async Task<string> Handle(MangaUploadCommand request, CancellationToken cancellationToken)
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
