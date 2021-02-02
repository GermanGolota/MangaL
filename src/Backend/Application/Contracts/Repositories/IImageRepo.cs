using Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IImageRepo
    {
        Task<string> GetChapterIdOfImage(string imageId, CancellationToken token = default);
        Task UpdatePictureLocation(string pictureId, string location, CancellationToken token = default);
        Task<List<PictureModel>> FindPicturesFor(string chapterId, CancellationToken token = default);
    }
}
