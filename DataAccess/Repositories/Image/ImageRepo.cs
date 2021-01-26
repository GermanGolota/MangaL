using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly ISQLClient _client;

        public ImageRepo(ISQLClient client)
        {
            this._client = client;
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
