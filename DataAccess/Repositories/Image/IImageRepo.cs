using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IImageRepo
    {
        Task<string> GetChapterIdOfImage(string imageId, CancellationToken token);
        Task UpdatePictureLocation(string pictureId, string location, CancellationToken token);
    }
}
