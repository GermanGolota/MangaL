﻿using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMangaWriteRepo
    {
        Task<string> SaveMangaInfoReturnId(MangaInfoModel info, CancellationToken token);
        Task<string> SaveChapterInfoReturnId(ChapterInfoModel info, CancellationToken token);
        Task<string> SavePictureInfoReturnId(PictureInfoModel info, CancellationToken token);
        Task UpdatePictureLocation(string pictureId, string location, CancellationToken token);
    }
}