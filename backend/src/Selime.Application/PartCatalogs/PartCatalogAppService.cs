using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Selime.Entities;
using Selime.PartCatalogs.Dto;
using Selime.Authorization;
using Selime.Flows;

namespace Selime.PartCatalogs
{
    public class PartCatalogAppService : AsyncCrudAppService<
        PartCatalog,
        PartCatalogDto,
        long,
        PagedPartCatalogResultRequestDto,
        CreatePartCatalogDto,
        PartCatalogDto>,
        IPartCatalogAppService
    {
        private readonly IFlowEngine _flowEngine;

        public PartCatalogAppService(IRepository<PartCatalog, long> repository, IFlowEngine flowEngine)
            : base(repository)
        {
            _flowEngine = flowEngine;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.PartCatalog_Read;
            GetAllPermissionName = PermissionNames.PartCatalog_Read;
            CreatePermissionName = PermissionNames.PartCatalog_Create;
            UpdatePermissionName = PermissionNames.PartCatalog_Update;
            DeletePermissionName = PermissionNames.PartCatalog_Delete;
        }

        protected override IQueryable<PartCatalog> CreateFilteredQuery(PagedPartCatalogResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.PartNumber != null && x.PartNumber.Contains(input.Keyword)) ||
                    (x.Description != null && x.Description.Contains(input.Keyword)) ||
                    (x.UnitOfMeasure != null && x.UnitOfMeasure.Contains(input.Keyword)))
                .WhereIf(!input.PartNumber.IsNullOrWhiteSpace(), x => x.PartNumber != null && x.PartNumber.Contains(input.PartNumber))
                .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description != null && x.Description.Contains(input.Description))
                .WhereIf(!input.UnitOfMeasure.IsNullOrWhiteSpace(), x => x.UnitOfMeasure != null && x.UnitOfMeasure.Contains(input.UnitOfMeasure))
                .WhereIf(input.StockQuantity.HasValue, x => x.StockQuantity == input.StockQuantity.Value);
        }

        public override async Task<PartCatalogDto> CreateAsync(CreatePartCatalogDto input)
        {
            var result = await base.CreateAsync(input);
            await _flowEngine.TriggerAsync("on-create", "PartCatalog", result);
            return result;
        }

        public override async Task<PartCatalogDto> UpdateAsync(PartCatalogDto input)
        {
            var result = await base.UpdateAsync(input);
            await _flowEngine.TriggerAsync("on-update", "PartCatalog", result);
            return result;
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
            await _flowEngine.TriggerAsync("on-delete", "PartCatalog", new { Id = input.Id });
        }
    }
}
