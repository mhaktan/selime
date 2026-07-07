using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Selime.AircraftTypes.Dto;

namespace Selime.AircraftTypes
{
    public interface IAircraftTypeAppService : IAsyncCrudAppService<
        AircraftTypeDto,
        long,
        PagedAircraftTypeResultRequestDto,
        CreateAircraftTypeDto,
        AircraftTypeDto>
    {
    }
}
