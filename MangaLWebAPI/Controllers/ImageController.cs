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
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> UploadImageForChapter(IFormFile file, [FromQuery] string chapterId,
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
