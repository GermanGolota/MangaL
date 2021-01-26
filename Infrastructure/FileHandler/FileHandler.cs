using DataAccess.Repositories;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.FileHandler
{
    public class FileHandler : IFileHandler
    {
        private readonly IChapterRepo _chapterRepo;
        private readonly AppConfiguration _config;

        public FileHandler(IChapterRepo chapterRepo, AppConfiguration config)
        {
            _chapterRepo = chapterRepo;
            this._config = config;
        }

        public async Task<string> CreateImagePath(string fileName, string chapterId, string imageId)
        {
            string fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(imageId, fileExtension);

            string mangaId = await _chapterRepo.FindMangaIdForChapter(chapterId, CancellationToken.None);

            string rootFolder = _config.GetContentRootPath();

            string mangaFolder = Path.Combine(rootFolder, "Mangas", $"{mangaId}");

            string chaptersFolder = Path.Combine(mangaFolder, "Chapters", $"{chapterId}");

            string imageFolderPath = Path.Combine(chaptersFolder, "Images");

            Directory.CreateDirectory(imageFolderPath);

            string filePath = Path.Combine(imageFolderPath, $"{newFileName}");

            return filePath;
        }

        public void SaveFileToLocation(IFormFile file, string location)
        {
            string directory = Path.GetDirectoryName(location);

            Directory.CreateDirectory(directory);

            using (FileStream fs = System.IO.File.Create(location))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }
    }
}
