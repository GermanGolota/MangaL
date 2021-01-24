using Core.Entities;
using DataAccess.DTOs;
using DataAccess.Repositories;
using FluentValidation;
using Infrastructure.Models;
using Infrastructure.Services;
using MangaLWebAPI.Mediatr;
using MangaLWebAPI.Models;
using MediatR;
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
        private readonly IMediator _mediator;

        public MangaAdditionController(IMangaWriteRepo repo, IMangaReadRepo readRepo, IMediator mediator)
        {
            this._repo = repo;
            this._readRepo = readRepo;
            this._mediator = mediator;
        }
        [HttpPost]
        [Route("addImage")]
        public async Task<IActionResult> UploadImageForManga(IFormFile file, [FromQuery] string chapterId,
            [FromQuery] int order, CancellationToken token)
        {
            UploadImageCommand command = new UploadImageCommand(file, chapterId, order);
            try
            {
                string imageId = await _mediator.Send(command, token);

                return Ok(imageId);
            }
            catch (TaskCanceledException)
            {
                return BadRequest("Canceled");
            }
            catch (ValidationException exc)
            {
                return BadRequest(exc.Message);
            }
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
