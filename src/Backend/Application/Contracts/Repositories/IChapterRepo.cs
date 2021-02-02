using Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IChapterRepo
    {
        Task<List<string>> GetImageIdsFor(string chapterId, CancellationToken token = default);

        Task<string> FindMangaIdForChapter(string chapterId, CancellationToken token = default);

        Task<ChapterModel> GetChapterBy(string chapterId, CancellationToken token = default);
    }
}
