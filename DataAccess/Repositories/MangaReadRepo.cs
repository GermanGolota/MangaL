using Core.Entities;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MangaReadRepo : IMangaReadRepo
    {
        private readonly ISQLClient _client;

        public MangaReadRepo(ISQLClient client)
        {
            this._client = client;
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

        public async Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId)
        {
            string sql = @"SELECT MangaTitle, Description FROM Mangas WHERE Id=@MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            List<MangaInfoModel> matches = await _client.LoadData<MangaInfoModel, dynamic>
                (sql, parameters, CancellationToken.None);
            if (matches.Count == 0)
            {
                throw new Exception("Can't find that manga");
            }
            var output = matches.First();

            return output;
        }
        private async Task<List<ChapterAdditionModel>> LoadChaptersInfoFor(string mangaId)
        {
            string sql = @"SELECT ChapterName, ChapterNumber, Id FROM Chapters WHERE MangaId = @MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            return await _client.LoadData<ChapterAdditionModel, dynamic>(sql, parameters, CancellationToken.None);
        }
    }
}
