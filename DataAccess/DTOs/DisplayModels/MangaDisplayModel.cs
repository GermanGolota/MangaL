using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOs
{
    public class MangaDisplayModel
    {
        public string MangaTitle { get; set; }
        public string Desription { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
        public IEnumerable<ChapterInfoModel> Chapters { get; set; }
    }
}
