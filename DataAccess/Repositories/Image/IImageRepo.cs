using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IImageRepo
    {
        Task<string> GetChapterIdOfImage(string imageId, CancellationToken token = default);
        Task UpdatePictureLocation(string pictureId, string location, CancellationToken token = default);
        Task<List<PictureModel>> FindPicturesFor(string chapterId, CancellationToken token = default);
    }
}
