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
                MangaDescription = infoModel.MangaDescription,
                MangaTitle = infoModel.MangaTitle,
                CoverPictureLocation = infoModel.CoverPictureLocation
            };

            List<ChapterInfoModel> chapters = await LoadChaptersInfoFor(mangaId, token);

            output.Chapters = chapters;

            List<CommentModel> comments = await LoadCommentsFor(mangaId, token);

            output.Comments = comments;

            return output;
        }
        private async Task<List<ChapterInfoModel>> LoadChaptersInfoFor(string mangaId, CancellationToken token)
        {
            string sql = @"SELECT ChapterName, ChapterNumber, Id as ChapterId FROM Chapters WHERE MangaId = @MangaId";

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
       

        public async Task<MangaInfoModel> FindMangaInfoByIDAsync(string mangaId, CancellationToken token)
        {
            string sql = @"SELECT MangaTitle, MangaDescription, CoverPictureLocation FROM Mangas WHERE Id=@MangaId";

            var parameters = new
            {
                MangaId = mangaId
            };

            List<MangaInfoModel> matches = await _client.LoadData<MangaInfoModel, dynamic>
                (sql, parameters, token);

            var output = matches.FirstOrDefault();

            return output;
        }

        public async Task<MangaDisplayModel> GetRandomManga(CancellationToken token)
        {
            string sql = @"SELECT * FROM Mangas
                            ORDER BY RAND()
                            LIMIT 1;";

            var results =  await  _client.LoadDataNoParam<MangaDisplayModel>(sql, token);

            var output = results.FirstOrDefault();

            List<ChapterInfoModel> chapters = await LoadChaptersInfoFor(output.Id, token);

            output.Chapters = chapters;

            List<CommentModel> comments = await LoadCommentsFor(output.Id, token);

            output.Comments = comments;

            return output;
        }
    }
}
