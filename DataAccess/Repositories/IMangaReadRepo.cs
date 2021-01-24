using Core.Entities;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMangaReadRepo
    {
        Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId);
        Task<string> FindMangaIdForChapter(string chapterId, CancellationToken token);
        Task<MangaDisplayModel> FindMangaById(string mangaId);
    }
}
