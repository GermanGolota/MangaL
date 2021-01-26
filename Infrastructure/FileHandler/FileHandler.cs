using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.FileHandler
{
    public class FileHandler : IFileHandler
    {
        public void SaveFileToLocation(IFormFile file, string location)
        {
            string directory = Path.GetDirectoryName(location);

            Directory.CreateDirectory(directory);

            using (FileStream fs = System.IO.File.Create(location))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }
    }
}
