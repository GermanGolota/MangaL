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

            List<Manga> matches = await _client.LoadData<Manga, dynamic>
                (sql, parameters, CancellationToken.None);
            if (matches.Count == 0)
            {
                //can't find manga
            }
            var firstMatch = matches.First();
            MangaInfoModel output = new MangaInfoModel
            {
                Desription = firstMatch.Description,
                MangaTitle = firstMatch.MangaTitle
            };

            return output;
        }
    }
}
