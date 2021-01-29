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
        Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId, CancellationToken token);
        Task<MangaDisplayModel> FindMangaById(string mangaId, CancellationToken token);
        Task<MangaDisplayModel> GetRandomManga(CancellationToken token);
    }
}
