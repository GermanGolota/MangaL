using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaLWebAPI.Models
{
    public class ChapterInfoUploadModel
    {
        public string MangaId { get; set; }
        public string ChapterName { get; set; }
        public string ChapterNumber { get; set; }
    }
}
