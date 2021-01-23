using Core;
using Core.Entities;
using DataAccess.Repositories;
using Infrastructure.Hashing;
using Infrastructure.ModelConverter;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        private readonly IModelConverter _converter;
        private readonly IUserRepo _repo;

        public UserServices(IModelConverter converter, IUserRepo repo)
        {
            this._converter = converter;
            this._repo = repo;
        }
        public async Task RegisterUser(UserRegistrationModel userModel)
        {
            User user = await _converter.ConvertUserFromDTOAsync(userModel);

            await _repo.SaveUserAsync(user);
        }
    }
}
