using DataAccess.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Queries
{
    public class FindChapterQuerie : IRequest<ChapterModel>
    {
        public string ChapterId { get; set; }

        public FindChapterQuerie(string chapterId)
        {
            ChapterId = chapterId;
        }
    }
}
