using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Hashing
{
    public interface IHasher
    {
        Task<string> Hash(string ToBeEncrypt);
    }
}
