﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class AppConfiguration
    {
        private readonly IConfiguration _config;

        public AppConfiguration(IConfiguration config)
        {
            this._config = config;
        }
        public string GetConnectionString()
        {
            string server = _config["DBServer"] ?? "localhost";
            string port = _config["Port"] ?? "3306";
            string dbName = _config["DBName"]?? "mangaldb";
            string username = _config["DBUsername"] ?? "root";
            string password = _config["DBPassword"] ??
                throw new Exception("Please provide a password");

            string connString = $"Server={server};Port={port};" +
                $"Database={dbName};Uid={username};Pwd={password}";

            return connString;
        }
        public string GetContentRootPath()
        {
            return "wwwroot";
        }
        public string GetAPIUrl()
        {
            return _config["APIUri"]??"https://localhost:5001/";
        }
    }
}
