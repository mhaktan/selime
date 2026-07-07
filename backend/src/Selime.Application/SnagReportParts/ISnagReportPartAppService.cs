using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Selime.SnagReportParts.Dto;

namespace Selime.SnagReportParts
{
    public interface ISnagReportPartAppService : IAsyncCrudAppService<
        SnagReportPartDto,
        long,
        PagedSnagReportPartResultRequestDto,
        CreateSnagReportPartDto,
        SnagReportPartDto>
    {
    }
}
