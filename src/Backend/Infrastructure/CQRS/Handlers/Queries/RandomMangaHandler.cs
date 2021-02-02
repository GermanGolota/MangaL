using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.FileHandler;
using Infrastructure.Queries;
using MediatR;

namespace Infrastructure.Handlers
{
    public class RandomMangaHandler : IRequestHandler<RandomMangaQuerie, MangaDisplayModel>
    {
        private readonly IMangaReadRepo _repo;
        private readonly IFileHandler _fileHandler;

        public RandomMangaHandler(IMangaReadRepo repo, IFileHandler fileHandler)
        {
            this._repo = repo;
            this._fileHandler = fileHandler;
        }
        public async Task<MangaDisplayModel> Handle(RandomMangaQuerie request, CancellationToken cancellationToken)
        {
            MangaDisplayModel output = await _repo.GetRandomManga(cancellationToken);
            output.CoverPictureLocation = _fileHandler.CreateFullUrlFromStored(output.CoverPictureLocation);
            return output;
        }
    }
}
