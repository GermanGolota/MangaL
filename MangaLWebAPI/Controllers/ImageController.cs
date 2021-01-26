using FluentValidation;
using Infrastructure.Commands;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{

    [ApiController]
    [Route("api/image")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        [Route("{imageId}")]
        public async Task<IActionResult> GetImagesForChapter([FromRoute] string imageId, CancellationToken token)
        {
            var querie = new FindImageQuerie(imageId);
            Stream stream = await _mediator.Send(querie, token);

            if (stream is null)
            {
                return BadRequest();
            }

            return File(stream, "application/octet-stream");
        }

        [HttpPost]
        [Route("add")]
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

    }
}
