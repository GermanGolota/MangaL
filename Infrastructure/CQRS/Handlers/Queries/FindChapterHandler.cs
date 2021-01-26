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
    public class FindChapterHandler : IRequestHandler<FindChapterQuerie, ChapterModel>
    {
        private readonly IChapterRepo _repo;

        public FindChapterHandler(IChapterRepo repo)
        {
            this._repo = repo;
        }
        public async Task<ChapterModel> Handle(FindChapterQuerie request, CancellationToken cancellationToken)
        {
            var output = await _repo.GetChapterBy(request.ChapterId, cancellationToken);
            return output;
        }
    }
}
