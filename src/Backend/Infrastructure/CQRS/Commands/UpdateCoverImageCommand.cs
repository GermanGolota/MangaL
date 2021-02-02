using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Commands
{
    public class UpdateCoverImageCommand : IRequest<string>
    {
        public string MangaId { get; set; }
        public IFormFile File { get; }

        public UpdateCoverImageCommand(string mangaId, IFormFile file)
        {
            MangaId = mangaId;
            File = file;
        }
    }
}
