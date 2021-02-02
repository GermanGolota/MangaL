using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Commands
{
    public class ChapterUploadHandler : IRequestHandler<ChapterUploadCommand, string>
    {
        private readonly IMangaWriteRepo _repo;
        private readonly IMapper _mapper;

        public ChapterUploadHandler(IMangaWriteRepo repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }
        public async Task<string> Handle(ChapterUploadCommand request, CancellationToken cancellationToken)
        {
            ChapterAdditionModel info = _mapper.Map<ChapterAdditionModel>(request);

            string id = await _repo.SaveChapterReturnId(info, cancellationToken);

            return id;
        }
    }
}
