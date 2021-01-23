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

        public async Task AddChapterToManga(ChapterModel chapterModel, string mangaId)
        {
            Chapter chapter = _converter.ConvertChapterFromDTO(chapterModel, mangaId);

            await _repo.SaveChapter(chapter);
        }

        public async Task<Manga> FindMangaByID(string mangaId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveManga(MangaModel mangaModel)
        {
            Manga manga = _converter.ConvertMangaFromDTO(mangaModel);

            await _repo.SaveManga(manga);
        }
    }
}
