using Core.Entities;
using DataAccess.Repositories;
using Infrastructure.ModelConverter;
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
        private readonly IModelConverter _converter;

        public MangaServices(IMangaRepo repo, IModelConverter converter)
        {
            this._repo = repo;
            this._converter = converter;
        }
        public async Task SaveManga(MangaAdditionModel mangaModel)
        {
            Manga manga = _converter.ConvertMangaFromDTO(mangaModel);

            await _repo.SaveManga(manga);
        }
    }
}
