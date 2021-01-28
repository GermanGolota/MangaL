using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Infrastructure.Queries
{
    public class ChapterImageIdsQuerie : IRequest<List<string>>
    {
        public string ChapterId { get; set; }
        public ChapterImageIdsQuerie(string chapterId)
        {
            ChapterId = chapterId;
        }
    }
}
