using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class MangaModel
    {
        public List<ChapterModel> Chapters { get; set; }
        public string MangaTitle { get; set; }
        public string Description { get; set; }
    }
}
