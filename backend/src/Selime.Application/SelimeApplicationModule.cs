using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Selime
{
    [DependsOn(typeof(SelimeCoreModule), typeof(AbpAutoMapperModule))]
    public class SelimeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.AddMaps(typeof(SelimeApplicationModule).GetAssembly());
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelimeApplicationModule).GetAssembly());
        }
    }
}
