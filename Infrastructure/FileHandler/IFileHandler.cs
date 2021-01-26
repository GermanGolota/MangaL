using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FileHandler
{
    public interface IFileHandler
    {
        void SaveFileToLocation(IFormFile file, string location);
        Task<string> CreateImagePath(string fileName, string chapterId, string imageId);
    }
}
