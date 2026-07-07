using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Selime.Entities;
using Selime.Stations.Dto;
using Selime.Authorization;
using Selime.Flows;

namespace Selime.Stations
{
    public class StationAppService : AsyncCrudAppService<
        Station,
        StationDto,
        long,
        PagedStationResultRequestDto,
        CreateStationDto,
        StationDto>,
        IStationAppService
    {
        private readonly IFlowEngine _flowEngine;

        public StationAppService(IRepository<Station, long> repository, IFlowEngine flowEngine)
            : base(repository)
        {
            _flowEngine = flowEngine;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Station_Read;
            GetAllPermissionName = PermissionNames.Station_Read;
            CreatePermissionName = PermissionNames.Station_Create;
            UpdatePermissionName = PermissionNames.Station_Update;
            DeletePermissionName = PermissionNames.Station_Delete;
        }

        protected override IQueryable<Station> CreateFilteredQuery(PagedStationResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.Code != null && x.Code.Contains(input.Keyword)) ||
                    (x.Name != null && x.Name.Contains(input.Keyword)) ||
                    (x.City != null && x.City.Contains(input.Keyword)))
                .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code != null && x.Code.Contains(input.Code))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name != null && x.Name.Contains(input.Name))
                .WhereIf(!input.City.IsNullOrWhiteSpace(), x => x.City != null && x.City.Contains(input.City));
        }

        public override async Task<StationDto> CreateAsync(CreateStationDto input)
        {
            var result = await base.CreateAsync(input);
            await _flowEngine.TriggerAsync("on-create", "Station", result);
            return result;
        }

        public override async Task<StationDto> UpdateAsync(StationDto input)
        {
            var result = await base.UpdateAsync(input);
            await _flowEngine.TriggerAsync("on-update", "Station", result);
            return result;
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
            await _flowEngine.TriggerAsync("on-delete", "Station", new { Id = input.Id });
        }
    }
}
