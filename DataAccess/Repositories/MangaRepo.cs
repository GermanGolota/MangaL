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
    public class MangaRepo : IMangaRepo
    {
        private readonly ISQLClient _client;

        public MangaRepo(ISQLClient client)
        {
            this._client = client;
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

            List<ChapterInfoModel> chapters =  await LoadChaptersInfoFor(mangaId);

            output.Chapters = chapters;

            return output;
        }
        private async Task<List<ChapterInfoModel>> LoadChaptersInfoFor(string mangaId)
        {
            string sql = @"SELECT ChapterName, ChapterNumber, Id FROM Chapters WHERE MangaId = @MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            return await _client.LoadData<ChapterInfoModel, dynamic>(sql, parameters, CancellationToken.None);
        }
    }
}
