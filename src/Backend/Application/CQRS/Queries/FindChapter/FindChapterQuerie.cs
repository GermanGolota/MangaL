using Application.DTOs;
using MediatR;

namespace Application.Queries
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
