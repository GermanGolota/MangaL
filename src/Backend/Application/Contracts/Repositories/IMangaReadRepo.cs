using Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IMangaReadRepo
    {
        Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId, CancellationToken token);
        Task<MangaDisplayModel> FindMangaById(string mangaId, CancellationToken token);
        Task<MangaDisplayModel> GetRandomManga(CancellationToken token);
    }
}
