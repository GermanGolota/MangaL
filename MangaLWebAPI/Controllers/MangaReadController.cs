using DataAccess.DTOs;
using DataAccess.Repositories;
using MangaLWebAPI.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Controllers
{
    [ApiController]
    [Route("api/manga/")]
    public class MangaReadController: ControllerBase
    {
        private readonly IMediator _mediator;

        public MangaReadController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MangaDisplayModel>> GetManga([FromRoute]string id)
        {
            var command = new FindMangaQuerie(id);
            var responce = await _mediator.Send(command);

            if(responce is null)
            {
                return BadRequest();
            }

            return Ok(responce);
        }
    }
}
