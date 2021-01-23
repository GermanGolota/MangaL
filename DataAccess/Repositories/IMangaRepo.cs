using Core.Entities;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMangaRepo
    {
        Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId);
        Task SaveManga(Manga manga);
    }
}
