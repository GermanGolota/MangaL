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

        public MangaAdditionController(IMangaWriteRepo repo)
        {
            this._repo = repo;
        }
        [Route("addImage")]
        public async Task<IActionResult> UploadImageForManga([FromBody] MangaImageUploadModel model,
            CancellationToken token)
        {
            IFormFile file = model.Picture;
            if (isNotValidFile(file))
            {
                return BadRequest();
            }

            var picture = new PictureInfoModel
            {
                ChapterId = model.ChapterId,
                PictureOrder = model.Order
            };

            var imageId = await _repo.SavePictureInfoReturnId(picture, token);

            string path = GetFilePath(file, imageId);

            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            await _repo.UpdatePictureLocation(imageId, path, token);

            return Ok(imageId);
        }
        private string GetFilePath(IFormFile file, string imageId)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(imageId, fileExtension);

            var filePath = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Images")).Root + $@"\{newFileName}";

            return filePath;
        }
        private bool isNotValidFile(IFormFile file)
        {
            return file == null || file.Length == 0;
        }
        [Route("addChapterInfo")]
        public async Task<IActionResult> UploadChapterInfo([FromBody] ChapterInfoUploadModel model, 
            CancellationToken token)
        {
            ChapterInfoModel info = new ChapterInfoModel
            {
                ChapterName = model.ChapterName,
                ChapterNumber = model.ChapterNumber,
                MangaId = model.MangaId
            };

            string id = await _repo.SaveChapterInfoReturnId(info, token);

            return Ok(id);
        }
        [Route("addMangaInfo")]
        public async Task<IActionResult> UploadMangaInfo([FromBody] MangaInfoUploadModel model, 
            CancellationToken token)
        {
            MangaInfoModel info = new MangaInfoModel
            {
                Desription = model.MangaDescription,
                MangaTitle = model.MangaTitle
            };

            string id = await _repo.SaveMangaInfoReturnId(info, token);

            return Ok(id);
        }
    }
}
