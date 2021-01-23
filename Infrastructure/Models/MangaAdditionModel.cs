using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class MangaAdditionModel
    {
        public List<PictureModel> PictureLinks { get; set; }
        public string MangaTitle { get; set; }
        public string Description { get; set; }
    }
}
