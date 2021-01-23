using Core;
using DataAccess.Repositories;
using Infrastructure.Hashing;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHasher _hasher;
        private readonly IUserRepo _repo;

        public UserServices(IHasher hasher, IUserRepo repo)
        {
            this._hasher = hasher;
            this._repo = repo;
        }
        public async Task RegisterUser(UserRegistrationModel userModel)
        {
            string userId = Guid.NewGuid().ToString();

            string passwordHash = await _hasher.Hash(userModel.Password);

            User user = new User
            {
                Id = userId,
                PasswordHash = passwordHash,
                Username = userModel.Username
            };

            await _repo.SaveUserAsync(user);
        }
    }
}
