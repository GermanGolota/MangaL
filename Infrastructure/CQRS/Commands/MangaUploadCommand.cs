using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Infrastructure.Commands
{
    public class MangaUploadCommand:IRequest<string>
    {
        public string MangaTitle { get; set; }
        public string MangaDescription { get; set; }
    }
}
