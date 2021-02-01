using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Manga
    {
        public string Id { get; set; }
        public string MangaTitle { get; set; }
        public string MangaDescription { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Chapter> Chapters { get; set; }
        public string CoverPictureLocation { get; set; }
    }
}
