using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Selime.Entities;
using Selime.Aircrafts.Dto;
using Selime.Authorization;
using Selime.Flows;

namespace Selime.Aircrafts
{
    public class AircraftAppService : AsyncCrudAppService<
        Aircraft,
        AircraftDto,
        long,
        PagedAircraftResultRequestDto,
        CreateAircraftDto,
        AircraftDto>,
        IAircraftAppService
    {
        private readonly IFlowEngine _flowEngine;

        public AircraftAppService(IRepository<Aircraft, long> repository, IFlowEngine flowEngine)
            : base(repository)
        {
            _flowEngine = flowEngine;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Aircraft_Read;
            GetAllPermissionName = PermissionNames.Aircraft_Read;
            CreatePermissionName = PermissionNames.Aircraft_Create;
            UpdatePermissionName = PermissionNames.Aircraft_Update;
            DeletePermissionName = PermissionNames.Aircraft_Delete;
        }

        protected override IQueryable<Aircraft> CreateFilteredQuery(PagedAircraftResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.Registration != null && x.Registration.Contains(input.Keyword)))
                .WhereIf(!input.Registration.IsNullOrWhiteSpace(), x => x.Registration != null && x.Registration.Contains(input.Registration))
                .WhereIf(input.ManufacturingYear.HasValue, x => x.ManufacturingYear == input.ManufacturingYear.Value)
                .WhereIf(input.Status.HasValue, x => x.Status == (Status)input.Status.Value)
                .WhereIf(input.AircraftTypeId.HasValue, x => x.AircraftTypeId == input.AircraftTypeId.Value)
                .WhereIf(input.StationId.HasValue, x => x.StationId == input.StationId.Value);
        }

        public override async Task<AircraftDto> CreateAsync(CreateAircraftDto input)
        {
            var result = await base.CreateAsync(input);
            await _flowEngine.TriggerAsync("on-create", "Aircraft", result);
            return result;
        }

        public override async Task<AircraftDto> UpdateAsync(AircraftDto input)
        {
            var result = await base.UpdateAsync(input);
            await _flowEngine.TriggerAsync("on-update", "Aircraft", result);
            return result;
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
            await _flowEngine.TriggerAsync("on-delete", "Aircraft", new { Id = input.Id });
        }
    }
}
