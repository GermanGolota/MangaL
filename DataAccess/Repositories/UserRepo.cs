using Core;
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
            //Names of the properties coincide, so there is no need to create parameter object
            string sql= @"INSERT INTO Users(Id, Username, PasswordHash)
                VALUES(@Id, @Username, @PasswordHash)";
            await _client.SaveData<User>(sql, user, CancellationToken.None);
        }
    }
}
