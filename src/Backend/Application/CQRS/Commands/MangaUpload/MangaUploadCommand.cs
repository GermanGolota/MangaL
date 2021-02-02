using MediatR;

namespace Application.Commands
{
    public class MangaUploadCommand:IRequest<string>
    {
        public string MangaTitle { get; set; }
        public string MangaDescription { get; set; }
    }
}
