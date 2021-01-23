using Core.Entities;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ModelConverter
{
    public interface IModelConverter
    {
        Manga ConvertMangaFromDTO(MangaAdditionModel dto);

        Task<User> ConvertUserFromDTOAsync(UserRegistrationModel dto);
    }
}
