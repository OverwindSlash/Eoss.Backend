using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Eoss.Backend.Authorization;
using Eoss.Backend.Configuration;
using Eoss.Backend.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace Eoss.Backend
{
    [DependsOn(
        typeof(BackendCoreModule),
        typeof(BackendDomainModule),
        typeof(AbpAutoMapperModule))]
    public class BackendApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BackendAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BackendApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
