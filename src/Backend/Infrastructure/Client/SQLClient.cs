using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SQLClient : ISQLClient
    {
        private readonly string connectionString;
        public SQLClient(string connString)
        {
            this.connectionString = connString;
        }
        public async Task<List<T>> LoadDataNoParam<T>(string sql, CancellationToken token)
        {
            using (IDbConnection connection = CreateConnection())
            {
                CommandDefinition command = new CommandDefinition(sql, cancellationToken: token);

                var data = await connection.QueryAsync<T>(command:command);

                return data.ToList();
            }
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, CancellationToken token)
        {
            using (IDbConnection connection = CreateConnection())
            {
                CommandDefinition command = new CommandDefinition(sql, parameters:parameters, cancellationToken:token);

                var data = await connection.QueryAsync<T>(command);

                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters, CancellationToken token)
        {
            using (IDbConnection connection = CreateConnection())
            {
                CommandDefinition command = new CommandDefinition(sql, parameters: parameters, cancellationToken: token);

                await connection.ExecuteAsync(command);
            }
        }
        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}