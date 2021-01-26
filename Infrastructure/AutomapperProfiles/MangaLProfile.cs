using AutoMapper;
using DataAccess.DTOs;
using Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

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
