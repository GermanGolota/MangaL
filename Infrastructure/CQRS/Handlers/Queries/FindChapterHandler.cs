using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Configuration;
using Infrastructure.Queries;
using MediatR;

namespace Infrastructure.Handlers
{
    public class FindChapterHandler : IRequestHandler<FindChapterQuerie, ChapterModel>
    {
        private readonly IChapterRepo _repo;
        private readonly AppConfiguration _config;

        public FindChapterHandler(IChapterRepo repo, AppConfiguration config)
        {
            this._repo = repo;
            this._config = config;
        }
        public async Task<ChapterModel> Handle(FindChapterQuerie request, CancellationToken cancellationToken)
        {
            ChapterModel output = await _repo.GetChapterBy(request.ChapterId, cancellationToken);
            string urlBase = _config.GetAPIUrl();
            foreach (var picture in output.Pictures)
            {
                picture.ImageLocation = GetFullUrl(urlBase, picture.ImageLocation);
            }
            return output;
        }
        private string GetFullUrl(string urlBase, string imageLocation)
        {
            string path = Path.Combine(urlBase, imageLocation);
            return new Uri(path).AbsoluteUri;
        }
    }
}
