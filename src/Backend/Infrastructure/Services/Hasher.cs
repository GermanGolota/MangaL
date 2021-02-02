using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class Hasher : IHasher
    {
        private readonly HashAlgorithm _algorithm;

        public Hasher(HashAlgorithm algorithm)
        {
            this._algorithm = algorithm;
        }
        public async Task<string> Hash(string ToBeEncrypt)
        {
            var bytes = Encoding.UTF8.GetBytes(ToBeEncrypt);
            var hash = _algorithm.ComputeHash(bytes);
            string output = Convert.ToBase64String(hash);
            return output;
        }
    }
}
