using DataAccess.DTOs;
using DataAccess.Repositories;
using FluentValidation;
using Infrastructure.Commands;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    [ApiController]
    [Route("api/manga/")]
    public class MangaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MangaController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MangaDisplayModel>> GetManga([FromRoute] string id)
        {
            var command = new FindMangaQuerie(id);
            var responce = await _mediator.Send(command);

            if (responce is null)
            {
                return BadRequest();
            }

            return Ok(responce);
        }


        [HttpPost]
        [Route("addInfo")]
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

        [HttpPost]
        [Route("updateCover/{mangaId}")]
        public async Task<IActionResult> UpdateCoveImage([FromRoute] string mangaId, IFormFile file,
            CancellationToken token)
        {
            var command = new UpdateCoverImageCommand(mangaId, file);
            string location = await _mediator.Send(command, token);
            return Ok(location);
        }
        [HttpGet]
        [Route("random")]
        public async Task<ActionResult<MangaDisplayModel>> GetRandomManga(CancellationToken cancellationToken)
        {
            var command = new RandomMangaQuerie();

            MangaDisplayModel manga = await _mediator.Send(command, cancellationToken);

            return manga;
        }

    }
}
