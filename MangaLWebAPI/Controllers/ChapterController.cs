using DataAccess.DTOs;
using FluentValidation;
using Infrastructure.Commands;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    [ApiController]
    [Route("api/chapter")]
    public class ChapterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChapterController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Route("{chapterId}/images/info")]
        public async Task<IActionResult> GetImageIdsForChapter
         ([FromRoute] string chapterId, CancellationToken token)
        {
            //TODO: Add validation
            var command = new ChapterImageIdsQuerie(chapterId);
            var responce = await _mediator.Send(command, token);

            return Ok(responce);
        }

        [HttpGet]
        [Route("{chapterId}")]
        public async Task<ActionResult<ChapterModel>> GetChapter([FromRoute] string chapterId, CancellationToken token)
        {
            //TODO: Add validation

            var querie = new FindChapterQuerie(chapterId);
            var response = await _mediator.Send(querie, token);
            return Ok(response);

        }
        [HttpPost]
        [Route("addInfo")]
        public async Task<IActionResult> UploadChapterInfo([FromBody] ChapterUploadCommand command,
            CancellationToken token)
        {
            //TODO: Add validation
            string chapterId = await _mediator.Send(command, token);

            return Ok(chapterId);

        }
    }
}
