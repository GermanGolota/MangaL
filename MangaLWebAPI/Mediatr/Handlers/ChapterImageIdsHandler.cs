using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repositories;
using MediatR;


namespace MangaLWebAPI.Mediatr
{
    public class ChapterImageIdsHandler : IRequestHandler<ChapterImageIdsQuerry, List<string>>
    {
        private readonly IChapterRepo _repo;

        public ChapterImageIdsHandler(IChapterRepo repo)
        {
            this._repo = repo;
        }
        public async Task<List<string>> Handle(ChapterImageIdsQuerry request, CancellationToken cancellationToken)
        {
            return await _repo.GetImageIdsFor(request.ChapterId, cancellationToken);
        }
    }
}
