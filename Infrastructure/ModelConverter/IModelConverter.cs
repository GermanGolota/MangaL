using Core.Entities;
using DataAccess.DTOs;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ModelConverter
{
    public interface IModelConverter
    {
        Task<User> ConvertUserFromDTOAsync(UserRegistrationModel dto);
    }
}
