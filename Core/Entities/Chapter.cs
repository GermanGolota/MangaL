using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Chapter
    {
        public string Id { get; set; }
        public string ChapterName { get; set; }
        public int ChapterNumber { get; set; }
        public string MangaId { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}
