using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class FindMangaHandler : IRequestHandler<FindMangaQuerie, MangaDisplayModel>
    {
        private readonly IMangaReadRepo _repo;
        private readonly IFileHandler _fileHandler;

        public FindMangaHandler(IMangaReadRepo repo, IFileHandler fileHandler)
        {
            this._repo = repo;
            this._fileHandler = fileHandler;
        }

        public async Task<MangaDisplayModel> Handle(FindMangaQuerie request, CancellationToken cancellationToken)
        {
            var manga = await _repo.FindMangaById(request.Id, cancellationToken);
            manga.CoverPictureLocation =  _fileHandler.CreateFullUrlFromStored(manga.CoverPictureLocation);
            return manga;
        }
    }
}
