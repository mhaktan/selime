using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Selime.AtaChapters.Dto;

namespace Selime.AtaChapters
{
    public interface IAtaChapterAppService : IAsyncCrudAppService<
        AtaChapterDto,
        long,
        PagedAtaChapterResultRequestDto,
        CreateAtaChapterDto,
        AtaChapterDto>
    {
    }
}
