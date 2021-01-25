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

        public async Task<MangaDisplayModel> FindMangaById(string mangaId, CancellationToken token)
        {
            var infoModel = await FindMangaInfoByIDAsync(mangaId, token);
            MangaDisplayModel output = new MangaDisplayModel
            {
                Desription = infoModel.Description,
                MangaTitle = infoModel.MangaName
            };

            List<ChapterInfoModel> chapters = await LoadChaptersInfoFor(mangaId, token);

            output.Chapters = chapters;

            List<CommentModel> comments = await LoadCommentsFor(mangaId, token);

            output.Comments = comments;

            return output;
        }
        private async Task<List<ChapterInfoModel>> LoadChaptersInfoFor(string mangaId, CancellationToken token)
        {
            string sql = @"SELECT ChapterName, ChapterNumber FROM Chapters WHERE MangaId = @MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            return await _client.LoadData<ChapterInfoModel, dynamic>(sql, parameters, token);
        }
        private async Task<List<CommentModel>> LoadCommentsFor(string mangaId, CancellationToken token)
        {
            string sql = @"SELECT Comments.CommentMessage, Users.Username as CommentorUsername FROM Comments 
                            LEFT JOIN Users On Comments.UserId = Users.Id
                            WHERE Comments.MangaId=@MangaId;";

            var parameters = new
            {
                MangaId = mangaId
            };

            return await _client.LoadData<CommentModel, dynamic>(sql, parameters, token);
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

        public async Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId, CancellationToken token)
        {
            string sql = @"SELECT MangaTitle, Description FROM Mangas WHERE Id=@MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            List<MangaInfoModel> matches = await _client.LoadData<MangaInfoModel, dynamic>
                (sql, parameters, token);

            var output = matches.FirstOrDefault();

            return output;
        }
        
    }
}
