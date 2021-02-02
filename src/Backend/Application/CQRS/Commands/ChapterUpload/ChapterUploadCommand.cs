using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands
{
    public class ChapterUploadCommand : IRequest<string>
    {
        public string MangaId { get; set; }
        public string ChapterName { get; set; }
        public int ChapterNumber { get; set; }
    }
}
