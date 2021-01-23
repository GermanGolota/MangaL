using Infrastructure.Models;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IMangaServices
    {
        Task SaveManga(MangaAdditionModel mangaModel);
        Task AddChapterToManga(ChapterModel chapter, string mangaId);
    }
}