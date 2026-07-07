using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Selime.Entities;
using Selime.AtaChapters.Dto;
using Selime.Authorization;
using Selime.Flows;

namespace Selime.AtaChapters
{
    public class AtaChapterAppService : AsyncCrudAppService<
        AtaChapter,
        AtaChapterDto,
        long,
        PagedAtaChapterResultRequestDto,
        CreateAtaChapterDto,
        AtaChapterDto>,
        IAtaChapterAppService
    {
        private readonly IFlowEngine _flowEngine;

        public AtaChapterAppService(IRepository<AtaChapter, long> repository, IFlowEngine flowEngine)
            : base(repository)
        {
            _flowEngine = flowEngine;
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.AtaChapter_Read;
            GetAllPermissionName = PermissionNames.AtaChapter_Read;
            CreatePermissionName = PermissionNames.AtaChapter_Create;
            UpdatePermissionName = PermissionNames.AtaChapter_Update;
            DeletePermissionName = PermissionNames.AtaChapter_Delete;
        }

        protected override IQueryable<AtaChapter> CreateFilteredQuery(PagedAtaChapterResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.AtaNumber != null && x.AtaNumber.Contains(input.Keyword)) ||
                    (x.Name != null && x.Name.Contains(input.Keyword)))
                .WhereIf(!input.AtaNumber.IsNullOrWhiteSpace(), x => x.AtaNumber != null && x.AtaNumber.Contains(input.AtaNumber))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name != null && x.Name.Contains(input.Name));
        }

        public override async Task<AtaChapterDto> CreateAsync(CreateAtaChapterDto input)
        {
            var result = await base.CreateAsync(input);
            await _flowEngine.TriggerAsync("on-create", "AtaChapter", result);
            return result;
        }

        public override async Task<AtaChapterDto> UpdateAsync(AtaChapterDto input)
        {
            var result = await base.UpdateAsync(input);
            await _flowEngine.TriggerAsync("on-update", "AtaChapter", result);
            return result;
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            await base.DeleteAsync(input);
            await _flowEngine.TriggerAsync("on-delete", "AtaChapter", new { Id = input.Id });
        }
    }
}
