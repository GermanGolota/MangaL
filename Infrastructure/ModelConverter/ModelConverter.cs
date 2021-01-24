using Core.Entities;
using DataAccess.DTOs;
using Infrastructure.Hashing;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ModelConverter
{
    public class ModelConverter : IModelConverter
    {
        private readonly IHasher _hasher;

        public ModelConverter(IHasher hasher)
        {
            this._hasher = hasher;
        }

        public Chapter ConvertChapterFromDTO(ChapterModel chapter, string mangaId)
        {
            List<Picture> pictures = new List<Picture>();

            foreach (var picture in chapter.Pictures)
            {
                pictures.Add(ConvertPictureFromDTO(picture, mangaId));
            }

            return new Chapter
            {
                ChapterName = chapter.ChapterName,
                ChapterNumber = chapter.ChapterNumber,
                Id = Guid.NewGuid().ToString(),
                MangaId = mangaId,
                Pictures = pictures
            };
        }
        private Picture ConvertPictureFromDTO(PictureModel picture, string mangaId)
        {
            string pictureId = Guid.NewGuid().ToString();
            return new Picture
            {
                Id = pictureId,
                ImageLocation = picture.ImageLocation,
                ChapterId = mangaId,
                PictureOrder = picture.PictureOrder
            };
        }
        public Manga ConvertMangaFromDTO(MangaModel mangaModel)
        {
            string mangaId = Guid.NewGuid().ToString();

            List<Chapter> chapters = new List<Chapter>();

            foreach (var chapter in mangaModel.Chapters)
            {
                chapters.Add(ConvertChapterFromDTO(chapter, mangaId));
            }

            Manga manga = new Manga
            {
                Id = mangaId,
                Description = mangaModel.Description,
                MangaTitle = mangaModel.MangaTitle,
                Chapters = chapters
            };

            return manga;
        }
        public async Task<User> ConvertUserFromDTOAsync(UserRegistrationModel userModel)
        {
            string userId = Guid.NewGuid().ToString();

            string passwordHash = await _hasher.Hash(userModel.Password);

            User user = new User
            {
                Id = userId,
                PasswordHash = passwordHash,
                Username = userModel.Username
            };

            return user;
        }

        public MangaModel ConvertFromInfoModel(MangaInfoModel infoModel)
        {
            List<ChapterModel> chapters = new List<ChapterModel>();

            foreach (var chapter in infoModel.Chapters)
            {
                chapters.Add(ConvertChapterInfoModel(chapter));
            }

            return new MangaModel
            {
                Description = infoModel.Desription,
                MangaTitle = infoModel.MangaTitle,
                Chapters = chapters
            };
        }
        private ChapterModel ConvertChapterInfoModel(ChapterInfoModel model)
        {
            return new ChapterModel
            {
                ChapterName = model.ChapterName,
                ChapterNumber = model.ChapterNumber
            };
        }
    }
}
