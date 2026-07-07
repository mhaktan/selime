using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Selime.SnagReports.Dto;

namespace Selime.SnagReports
{
    public interface ISnagReportAppService : IAsyncCrudAppService<
        SnagReportDto,
        long,
        PagedSnagReportResultRequestDto,
        CreateSnagReportDto,
        SnagReportDto>
    {
        Task<SnagReportDto> ChangeStatusAsync(long id, ChangeStatusInput input);
    }
}
