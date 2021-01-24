using Core.Entities;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Models;
using Infrastructure.Services;
using MangaLWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    [Route("api/manga/")]
    public class MangaAdditionController : ControllerBase
    {
        private readonly IMangaWriteRepo _repo;
        private readonly IMangaReadRepo _readRepo;

        public MangaAdditionController(IMangaWriteRepo repo, IMangaReadRepo readRepo)
        {
            this._repo = repo;
            this._readRepo = readRepo;
        }
        [HttpPost]
        [Route("addImage")]
        public async Task<IActionResult> UploadImageForManga(IFormFile file, [FromQuery]string chapterId,
            [FromQuery] int order, CancellationToken token)
        {
            if (isNotValidFile(file))
            {
                return BadRequest();
            }

            var picture = new PictureAdditionModel
            {
                ChapterId = chapterId,
                PictureOrder = order
            };

            string imageId = await _repo.SavePictureReturnId(picture, token);

            string mangaId = await _readRepo.FindMangaIdForChapter(chapterId, token);

            string path = GetFilePath(file, imageId, chapterId, mangaId);

            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            await _repo.UpdatePictureLocation(imageId, path, token);

            return Ok(imageId);
        }
        private string GetFilePath(IFormFile file, string imageId, string chapterId, string mangaId)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(imageId, fileExtension);

            string rootFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot");

            string mangaFolder = Path.Combine(rootFolder, "Mangas", $"{mangaId}");

            string chaptersFolder = Path.Combine(mangaFolder, "Chapters", $"{chapterId}");

            string imageFolderPath = Path.Combine(chaptersFolder, "Images");

            Directory.CreateDirectory(imageFolderPath);

            string filePath = Path.Combine(imageFolderPath, $"{newFileName}");

            return filePath;
        }
        private bool isNotValidFile(IFormFile file)
        {
            return file == null || file.Length == 0;
        }
        [HttpPost]
        [Route("addChapterInfo")]
        public async Task<IActionResult> UploadChapterInfo([FromBody] ChapterInfoUploadModel model, 
            CancellationToken token)
        {
            ChapterAdditionModel info = new ChapterAdditionModel
            {
                ChapterName = model.ChapterName,
                ChapterNumber = model.ChapterNumber,
                MangaId = model.MangaId
            };

            string id = await _repo.SaveChapterReturnId(info, token);

            return Ok(id);
        }
        [HttpPost]
        [Route("addMangaInfo")]
        public async Task<IActionResult> UploadMangaInfo([FromBody] MangaInfoUploadModel model, 
            CancellationToken token)
        {
            MangaAdditionModel info = new MangaAdditionModel
            {
                Desription = model.MangaDescription,
                MangaTitle = model.MangaTitle
            };

            string id = await _repo.SaveMangaReturnId(info, token);

            return Ok(id);
        }
    }
}
