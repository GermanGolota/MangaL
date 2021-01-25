using Core.Entities;
using DataAccess.DTOs;
using DataAccess.Repositories;
using FluentValidation;
using Infrastructure.Models;
using Infrastructure.Services;
using MangaLWebAPI.Mediatr;
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
        private readonly IMediator _mediator;

        public MangaAdditionController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Route("addImage")]
        public async Task<IActionResult> UploadImageForManga(IFormFile file, [FromQuery] string chapterId,
            [FromQuery] int order, CancellationToken token)
        {
            ImageUploadCommand command = new ImageUploadCommand(file, chapterId, order);
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
        public async Task<IActionResult> UploadChapterInfo([FromBody] ChapterUploadCommand command,
            CancellationToken token)
        {
            //TODO: Add validation
            try
            {
                string chapterId = await _mediator.Send(command, token);

                return Ok(chapterId);
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
        [Route("addMangaInfo")]
        public async Task<IActionResult> UploadMangaInfo([FromBody] MangaUploadCommand command,
            CancellationToken token)
        {
            //TODO: Add validation
            try
            {
                string mangaId = await _mediator.Send(command, token);

                return Ok(mangaId);
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
    }
}
