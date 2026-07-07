using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Selime.Entities;
using Selime.SnagReportParts.Dto;
using Selime.Authorization;
using Selime.Flows;

namespace Selime.SnagReportParts
{
    public class SnagReportPartAppService : AsyncCrudAppService<
        SnagReportPart,
        SnagReportPartDto,
        long,
        PagedSnagReportPartResultRequestDto,
        CreateSnagReportPartDto,
        SnagReportPartDto>,
        ISnagReportPartAppService
    {
        private readonly IFlowEngine _flowEngine;

        public SnagReportPartAppService(IRepository<SnagReportPart, long> repository, IFlowEngine flowEngine)
            : base(repository)
        {
            _flowEngine = flowEngine;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.SnagReportPart_Read;
            GetAllPermissionName = PermissionNames.SnagReportPart_Read;
            CreatePermissionName = PermissionNames.SnagReportPart_Create;
            UpdatePermissionName = PermissionNames.SnagReportPart_Update;
            DeletePermissionName = PermissionNames.SnagReportPart_Delete;
        }

        protected override IQueryable<SnagReportPart> CreateFilteredQuery(PagedSnagReportPartResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.SerialNumber != null && x.SerialNumber.Contains(input.Keyword)))
                .WhereIf(!input.SerialNumber.IsNullOrWhiteSpace(), x => x.SerialNumber != null && x.SerialNumber.Contains(input.SerialNumber))
                .WhereIf(input.QuantityUsed.HasValue, x => x.QuantityUsed == input.QuantityUsed.Value)
                .WhereIf(input.SnagReportId.HasValue, x => x.SnagReportId == input.SnagReportId.Value)
                .WhereIf(input.PartCatalogId.HasValue, x => x.PartCatalogId == input.PartCatalogId.Value);
        }

        public override async Task<SnagReportPartDto> CreateAsync(CreateSnagReportPartDto input)
        {
            var result = await base.CreateAsync(input);
            await _flowEngine.TriggerAsync("on-create", "SnagReportPart", result);
            return result;
        }

        public override async Task<SnagReportPartDto> UpdateAsync(SnagReportPartDto input)
        {
            var result = await base.UpdateAsync(input);
            await _flowEngine.TriggerAsync("on-update", "SnagReportPart", result);
            return result;
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
            await _flowEngine.TriggerAsync("on-delete", "SnagReportPart", new { Id = input.Id });
        }
    }
}
