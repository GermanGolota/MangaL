using Core;
using Core.Entities;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ISQLClient _client;

        public UserRepo(ISQLClient client)
        {
            this._client = client;
        }
        public async Task SaveUserAsync(User user)
        {
            string sql= @"INSERT INTO Users(Id, Username, PasswordHash)
                VALUES(@Id, @Username, @PasswordHash)";
            await _client.SaveData<User>(sql, user, CancellationToken.None);
        }
    }
}
