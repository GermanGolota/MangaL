using Application.Commands;
using Application.DTOs;
using AutoMapper;

namespace Infrastructure.AutomapperProfiles
{
    class MangaLProfile : Profile
    {
        public MangaLProfile()
        {
            CreateMap<ChapterUploadCommand, ChapterAdditionModel>();
        }
    }
}
