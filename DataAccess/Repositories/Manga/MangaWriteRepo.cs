using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MangaWriteRepo : IMangaWriteRepo
    {
        private readonly ISQLClient _client;

        public MangaWriteRepo(ISQLClient client)
        {
            this._client = client;
        }
        public async Task<string> SaveMangaReturnId(MangaAdditionModel info, CancellationToken token)
        {
            string id = CreateUniqueId();
            string sql = @"INSERT INTO Mangas(Id, MangaTitle, MangaDescription) VALUES(@Id, @Title, @Description)";

            var parameters = new
            {
                Id = id,
                Title = info.MangaTitle,
                Description = info.Desription
            };

            await _client.SaveData<dynamic>(sql, parameters, token);

            return id;
        }

        public async Task<string> SaveChapterReturnId(ChapterAdditionModel info, CancellationToken token)
        {
            string id = CreateUniqueId();
            string sql = @"INSERT INTO Chapters(Id, ChapterName, ChapterNumber, MangaId)" +
                           @"VALUES(@Id, @ChapterName, @ChapterNumber, @MangaId)";
            var parameters = new
            {
                Id = id,
                ChapterName = info.ChapterName,
                ChapterNumber = info.ChapterNumber,
                MangaId = info.MangaId
            };

            await _client.SaveData<dynamic>(sql, parameters, token);

            return id;
        }

        public async Task<string> SavePictureReturnId(PictureAdditionModel info, CancellationToken token)
        {
            string id = CreateUniqueId();
            string sql = @"INSERT INTO Pictures(Id, ChapterId, PictureOrder, ImageLocation)" +
                            @"VALUES(@Id, @ChapterId, @PictureOrder, @ImageLocation)";
            var parameters = new
            {
                Id = id,
                ChapterId = info.ChapterId,
                PictureOrder = info.PictureOrder,
                ImageLocation = info.PictureLocation
            };

            await _client.SaveData<dynamic>(sql, parameters, token);

            return id;
        }
        private string CreateUniqueId()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task UpdateCoverPictureLocation(string coverPictureLocation, string mangaId, CancellationToken token)
        {
            string sql = @"UPDATE Mangas SET CoverPictureLocation = @CoverPicture WHERE Mangas.Id = @MangaId";

            var parameters = new
            {
                CoverPicture = coverPictureLocation,
                MangaId = mangaId
            };

            await _client.SaveData<dynamic>(sql, parameters, token);
        }
    }
}
