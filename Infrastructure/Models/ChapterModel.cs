using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class ChapterModel
    {
        public string ChapterName { get; set; }
        public int ChapterNumber { get; set; }
        public List<PictureModel> Pictures { get; set; }
    }
}
