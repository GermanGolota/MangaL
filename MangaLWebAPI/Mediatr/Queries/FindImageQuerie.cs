using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MangaLWebAPI.Mediatr
{
    public class FindImageQuerie:IRequest<Stream>
    {
        public string ImageId { get; set; }
        public FindImageQuerie(string imageId)
        {
            ImageId = imageId;
        }
    }
}
