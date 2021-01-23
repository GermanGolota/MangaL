using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Manga
    {
        public string Id { get; set; }
        public string MangaTitle { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}
