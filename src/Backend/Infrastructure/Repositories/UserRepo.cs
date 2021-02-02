using Application.Contracts;
using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
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
