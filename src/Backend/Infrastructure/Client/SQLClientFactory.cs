using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class SQLClientFactory : ISQLClientFactory
    {
        private readonly AppConfiguration _config;

        public SQLClientFactory(AppConfiguration config)
        {
            this._config = config;
        }
        public ISQLClient CreateClient()
        {
            string connStr = _config.GetConnectionString();
            return new SQLClient(connStr);
        }
    }
}
