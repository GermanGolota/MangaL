using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class FindMangaQuerie:IRequest<MangaDisplayModel>
    {
        public string Id { get; set; }
        public FindMangaQuerie(string id)
        {
            Id = id;
        }
    }
}
