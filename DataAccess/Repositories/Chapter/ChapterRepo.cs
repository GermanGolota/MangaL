using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChapterRepo : IChapterRepo
    {
        private readonly ISQLClient _client;

        public ChapterRepo(ISQLClient client)
        {
            this._client = client;
        }
        public async Task<List<string>> GetImageIdsFor(string chapterId, CancellationToken token)
        {
            string sql = @"SELECT Id FROM Pictures WHERE ChapterId = @ChapterId";

            var parameters = new
            {
                ChapterId = chapterId
            };

            List<string> ids = await _client.LoadData<string, dynamic>(sql, parameters, token);

            return ids;
        }

        public async Task<string> FindMangaIdForChapter(string chapterId, CancellationToken token)
        {
            string sql = @"SELECT MangaId FROM Chapters WHERE Id = @ChapterId";

            var parameters = new
            {
                ChapterId = chapterId
            };

            var result = await _client.LoadData<string, dynamic>(sql, parameters, token);
            //There can be two chapters with the same id, because id is a guid
            string output = result.FirstOrDefault();

            return output;
        }
    }
}
