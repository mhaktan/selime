using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Selime.Entities;
using Selime.AircraftTypes.Dto;
using Selime.Authorization;
using Selime.Flows;

namespace Selime.AircraftTypes
{
    public class AircraftTypeAppService : AsyncCrudAppService<
        AircraftType,
        AircraftTypeDto,
        long,
        PagedAircraftTypeResultRequestDto,
        CreateAircraftTypeDto,
        AircraftTypeDto>,
        IAircraftTypeAppService
    {
        private readonly IFlowEngine _flowEngine;

        public AircraftTypeAppService(IRepository<AircraftType, long> repository, IFlowEngine flowEngine)
            : base(repository)
        {
            _flowEngine = flowEngine;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.AircraftType_Read;
            GetAllPermissionName = PermissionNames.AircraftType_Read;
            CreatePermissionName = PermissionNames.AircraftType_Create;
            UpdatePermissionName = PermissionNames.AircraftType_Update;
            DeletePermissionName = PermissionNames.AircraftType_Delete;
        }

        protected override IQueryable<AircraftType> CreateFilteredQuery(PagedAircraftTypeResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.TypeCode != null && x.TypeCode.Contains(input.Keyword)) ||
                    (x.Manufacturer != null && x.Manufacturer.Contains(input.Keyword)))
                .WhereIf(!input.TypeCode.IsNullOrWhiteSpace(), x => x.TypeCode != null && x.TypeCode.Contains(input.TypeCode))
                .WhereIf(!input.Manufacturer.IsNullOrWhiteSpace(), x => x.Manufacturer != null && x.Manufacturer.Contains(input.Manufacturer))
                .WhereIf(input.SeatCapacity.HasValue, x => x.SeatCapacity == input.SeatCapacity.Value);
        }

        public override async Task<AircraftTypeDto> CreateAsync(CreateAircraftTypeDto input)
        {
            var result = await base.CreateAsync(input);
            await _flowEngine.TriggerAsync("on-create", "AircraftType", result);
            return result;
        }

        public override async Task<AircraftTypeDto> UpdateAsync(AircraftTypeDto input)
        {
            var result = await base.UpdateAsync(input);
            await _flowEngine.TriggerAsync("on-update", "AircraftType", result);
            return result;
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
            await _flowEngine.TriggerAsync("on-delete", "AircraftType", new { Id = input.Id });
        }
    }
}
