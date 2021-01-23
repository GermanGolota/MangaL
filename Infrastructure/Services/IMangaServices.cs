using Core.Entities;
using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IMangaServices
    {
        Task SaveManga(MangaModel mangaModel);
        Task AddChapterToManga(ChapterModel chapter, string mangaId);
        Task<MangaModel> FindMangaInfoByID(string mangaId);
    }
}