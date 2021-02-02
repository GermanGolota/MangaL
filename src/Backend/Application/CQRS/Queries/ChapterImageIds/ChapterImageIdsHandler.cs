using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using MediatR;


namespace Application.Queries
{
    public class ChapterImageIdsHandler : IRequestHandler<ChapterImageIdsQuerie, List<string>>
    {
        private readonly IChapterRepo _repo;

        public ChapterImageIdsHandler(IChapterRepo repo)
        {
            this._repo = repo;
        }
        public async Task<List<string>> Handle(ChapterImageIdsQuerie request, CancellationToken cancellationToken)
        {
            return await _repo.GetImageIdsFor(request.ChapterId, cancellationToken);
        }
    }
}
