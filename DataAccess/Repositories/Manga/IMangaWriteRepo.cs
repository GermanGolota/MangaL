using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMangaWriteRepo
    {
        Task<string> SaveMangaReturnId(MangaAdditionModel info, CancellationToken token);
        Task<string> SaveChapterReturnId(ChapterAdditionModel info, CancellationToken token);
        Task<string> SavePictureReturnId(PictureAdditionModel info, CancellationToken token);
    }
}
