﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class MangaDisplayModel
    {
        public string MangaTitle { get; set; }
        public string MangaDescription { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
        public IEnumerable<ChapterInfoModel> Chapters { get; set; }
        public string CoverPictureLocation { get; set; }
        public string Id { get; set; }
    }
}
