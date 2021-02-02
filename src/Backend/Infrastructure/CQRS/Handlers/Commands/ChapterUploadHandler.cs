using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DTOs;
using DataAccess.Repositories;
using Infrastructure.Commands;
using MediatR;

namespace Infrastructure.Handlers
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
