using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repositories;
using MangaLWebAPI.Configuration;
using MediatR;

namespace MangaLWebAPI.Mediatr
{
    public class FindImageHandler : IRequestHandler<FindImageQuerie, Stream>
    {
        private readonly IImageRepo _repo;
        private readonly IChapterRepo _chapterRepo;
        private readonly AppConfiguration _config;

        public FindImageHandler(IImageRepo repo,IChapterRepo chapterRepo, AppConfiguration config)
        {
            _repo = repo;
            this._chapterRepo = chapterRepo;
            this._config = config;
        }
        public async Task<Stream> Handle(FindImageQuerie request, CancellationToken cancellationToken)
        {
            string imageId = request.ImageId;
            
            string chapterId = await _repo.GetChapterIdOfImage(imageId, cancellationToken);

            string mangaId = await _chapterRepo.FindMangaIdForChapter(chapterId, cancellationToken);

            string rootFolder = _config.GetContentRootPath();

            string imagesFolderPath = Path.Combine(rootFolder, "Mangas", mangaId, "Chapters", chapterId, "Images");

            string[] potentialFileExtensions = new string[]
            {
                "jpg","png","jpeg"
            };
            foreach (var fileExtension in potentialFileExtensions)
            {
                string fileName = Path.ChangeExtension(imageId, fileExtension);
                string filePath = Path.Combine(imagesFolderPath, fileName);
                if(File.Exists(filePath))
                {
                    Stream stream = File.OpenRead(filePath);
                    return stream;
                }
            }
            return Stream.Null;
            
        }
    }
}
