using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.FileHandler
{
    public interface IFileHandler
    {
        void SaveFileToLocation(IFormFile file, string location);
    }
}
