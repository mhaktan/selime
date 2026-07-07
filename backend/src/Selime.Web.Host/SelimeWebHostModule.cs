using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Selime.EntityFrameworkCore;

namespace Selime.Web.Host
{
    [DependsOn(typeof(SelimeApplicationModule), typeof(SelimeEntityFrameworkCoreModule), typeof(AbpAspNetCoreModule))]
    public class SelimeWebHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            // Expose all AppServices as dynamic API controllers
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SelimeApplicationModule).GetAssembly(),
                    moduleName: "app",
                    useConventionalHttpVerbs: true
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelimeWebHostModule).GetAssembly());
        }
    }
}
