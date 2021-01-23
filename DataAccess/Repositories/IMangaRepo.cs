using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMangaRepo
    {
        Task<Manga> FindMangaInfoByIDAsync(string mangaId);
    }
}
