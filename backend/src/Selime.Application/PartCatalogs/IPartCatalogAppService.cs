using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Selime.PartCatalogs.Dto;

namespace Selime.PartCatalogs
{
    public interface IPartCatalogAppService : IAsyncCrudAppService<
        PartCatalogDto,
        long,
        PagedPartCatalogResultRequestDto,
        CreatePartCatalogDto,
        PartCatalogDto>
    {
    }
}
