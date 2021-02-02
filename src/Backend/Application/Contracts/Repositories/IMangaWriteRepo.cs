using Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IMangaWriteRepo
    {
        Task<string> SaveMangaReturnId(MangaAdditionModel info, CancellationToken token);
        Task<string> SaveChapterReturnId(ChapterAdditionModel info, CancellationToken token);
        Task<string> SavePictureReturnId(PictureAdditionModel info, CancellationToken token);
        Task UpdateCoverPictureLocation(string coverPictureLocation, string mangaId, CancellationToken token);
    }
}
