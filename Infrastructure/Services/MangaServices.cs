using Core.Entities;
using DataAccess.Repositories;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MangaServices : IMangaServices
    {
        private readonly IMangaRepo _repo;

        public MangaServices(IMangaRepo repo)
        {
            this._repo = repo;
        }
        public async Task SaveManga(MangaAdditionModel mangaModel)
        {
            string mangaId = Guid.NewGuid().ToString();

            List<Picture> pictures = new List<Picture>();

            foreach (var picture in mangaModel.PictureLinks)
            {
                string pictureId = Guid.NewGuid().ToString();
                pictures.Add(new Picture
                {
                    Id = pictureId,
                    ImageLocation = picture.ImageLink,
                    MangaId = mangaId,
                    PictureOrder = picture.Order
                });
            }

            Manga manga = new Manga
            {
                Id = mangaId,
                Description = mangaModel.Description,
                MangaTitle = mangaModel.MangaTitle,
                Pictures = pictures
            };

            await _repo.SaveManga(manga);
        }
    }
}
