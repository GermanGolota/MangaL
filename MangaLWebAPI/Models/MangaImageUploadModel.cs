using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Models
{
    public class MangaImageUploadModel
    {
        public string ChapterId { get; set; }
        public IFormFile Picture { get; set; }
        public int Order { get; set; }
    }
}
