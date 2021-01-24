using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOs
{
    public class MangaAdditionModel
    {
        public string MangaTitle { get; set; }
        public string Desription { get; set; }
        public IEnumerable<ChapterAdditionModel> Chapters { get; set; }
    }
}
