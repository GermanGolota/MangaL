using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MangaLWebAPI.Mediatr
{
    public class ChapterImageIdsQuerry : IRequest<List<string>>
    {
        public string ChapterId { get; set; }
        public ChapterImageIdsQuerry(string chapterId)
        {
            ChapterId = chapterId;
        }
    }
}
