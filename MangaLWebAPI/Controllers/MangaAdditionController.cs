using Core.Entities;
using DataAccess.Repositories;
using Infrastructure.Models;
using Infrastructure.Services;
using MangaLWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    [Route("api/manga/")]
    public class MangaAdditionController : ControllerBase
    {
        private readonly IMangaRepo _repo;

        public MangaAdditionController(IMangaRepo repo)
        {
            this._repo = repo;
        }
        [Route("addImage")]
        public async Task<IActionResult> UploadImageForManga([FromBody] MangaImageUploadModel model)
        {
            var file = model.Picture;

            var imageId = Guid.NewGuid().ToString();

            if (isNotValidFile(file))
            {
                return BadRequest();
            }

            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName);

            var newFileName = String.Concat(imageId, fileExtension);

            var filepath = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Images")).Root + $@"\{newFileName}";

            using (FileStream fs = System.IO.File.Create(filepath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            var picture = new Picture
            {
                ChapterId = model.ChapterId,
                Id = imageId,
                PictureOrder = model.Order,
                ImageLocation = filepath
            };

            await _repo.SavePicture(picture);

            return Ok();
        }
        [Route("addChapterInfo")]
        public async Task<IActionResult> UploadChapterInfo([FromBody] ChapterInfoUploadModel model)
        {

        }
        [Route("addMangaInfo")]
        public async Task<IActionResult> UploadMangaInfo([FromBody] ChapterInfoUploadModel model)
        {

        }
        private bool isNotValidFile(Microsoft.AspNetCore.Http.IFormFile file)
        {
            return file == null || file.Length == 0;
        }
    }
}
