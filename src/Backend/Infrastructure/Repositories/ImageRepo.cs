using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Application.Contracts;
using Application.DTOs;

namespace Infrastructure.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly ISQLClient _client;

        public ImageRepo(ISQLClient client)
        {
            this._client = client;
        }

        public async Task<List<PictureModel>> FindPicturesFor(string chapterId, CancellationToken token)
        {
            string sql = @"SELECT PictureOrder, ImageLocation FROM Pictures WHERE ChapterId = @ChapterId";

            var parameters = new
            {
                ChapterId = chapterId
            };

            List<PictureModel> pictures = await _client.LoadData<PictureModel, dynamic>(sql, parameters, token);

            return pictures;
        }

        public async Task<string> GetChapterIdOfImage(string imageId, CancellationToken token)
        {
            string sql = @"SELECT ChapterId FROM Pictures WHERE Id = @ImageId";

            var parameters = new
            {
                ImageId = imageId
            };

            IEnumerable<string> matches = await _client.LoadData<string, dynamic>(sql, parameters, token);

            return matches.FirstOrDefault();
        }

        public async Task UpdatePictureLocation(string pictureId, string location, CancellationToken token)
        {
            string sql = @"UPDATE Pictures
                           SET ImageLocation = @ImageLocation
                           WHERE Id = @PictureId";

            var parameters = new
            {
                ImageLocation = location,
                PictureId = pictureId
            };

            await _client.SaveData<dynamic>(sql, parameters, token);
        }
    }
}
