using MangaLWebAPI.Mediatr;
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
            var command = new ChapterImageIdsQuerry(chapterId);
            var responce = await _mediator.Send(command, token);

            return Ok(responce);
        }
    }
}
