using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Mediatr
{
    public class UploadImageCommand:IRequest<string>
    {
        public string ChapterId { get; set; }
        public int Order { get; set; }
        public IFormFile File { get; set; }
        public UploadImageCommand(IFormFile file, string chapterId, int order)
        {
            File = file;
            ChapterId = chapterId;
            Order = order;
        }
    }
}
