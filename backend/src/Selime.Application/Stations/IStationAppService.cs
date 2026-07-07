using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Selime.Stations.Dto;

namespace Selime.Stations
{
    public interface IStationAppService : IAsyncCrudAppService<
        StationDto,
        long,
        PagedStationResultRequestDto,
        CreateStationDto,
        StationDto>
    {
    }
}
