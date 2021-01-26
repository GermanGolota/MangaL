﻿using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IChapterRepo
    {
        Task<List<string>> GetImageIdsFor(string chapterId, CancellationToken token = default);

        Task<string> FindMangaIdForChapter(string chapterId, CancellationToken token = default);

        Task<ChapterModel> GetChapterBy(string chapterId, CancellationToken token = default);
    }
}
