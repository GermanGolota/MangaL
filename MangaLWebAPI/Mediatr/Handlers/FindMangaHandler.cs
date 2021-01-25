using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using MediatR;

namespace MangaLWebAPI.Mediatr
{
    public class FindMangaHandler : IRequestHandler<FindMangaQuerie, MangaDisplayModel>
    {
        private readonly IMangaReadRepo _repo;

        public FindMangaHandler(IMangaReadRepo repo)
        {
            this._repo = repo;
        }

        public Task<MangaDisplayModel> Handle(FindMangaQuerie request, CancellationToken cancellationToken)
        {
            return _repo.FindMangaById(request.Id, cancellationToken);
        }
    }
}
