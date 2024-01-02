using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Eoss.Backend.Domain
{
    [DependsOn(
        typeof(BackendCoreModule))]
    public class BackendDomainModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BackendDomainModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
