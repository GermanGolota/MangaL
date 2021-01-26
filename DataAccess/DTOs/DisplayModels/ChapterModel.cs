using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOs
{
    public class ChapterModel
    {
        public string ChapterName { get; set; }
        public int ChapterId { get; set; }
        public IEnumerable<PictureModel> Pictures { get; set; }
    }
}
