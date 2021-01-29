using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Queries;
using MediatR;

namespace Infrastructure.Handlers
{
    public class RandomMangaHandler : IRequestHandler<RandomMangaQuerie, MangaDisplayModel>
    {
        private readonly IMangaReadRepo _repo;

        public RandomMangaHandler(IMangaReadRepo repo)
        {
            this._repo = repo;
        }
        public async Task<MangaDisplayModel> Handle(RandomMangaQuerie request, CancellationToken cancellationToken)
        {
            MangaDisplayModel output = await _repo.GetRandomManga(cancellationToken);
            return output;
        }
    }
}
