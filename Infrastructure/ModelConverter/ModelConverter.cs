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
    }
}
