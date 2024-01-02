using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Eoss.Backend.EntityFrameworkCore;
using Eoss.Backend.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Eoss.Backend.Web.Tests
{
    [DependsOn(
        typeof(BackendWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BackendWebTestModule : AbpModule
    {
        public BackendWebTestModule(BackendEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BackendWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BackendWebMvcModule).Assembly);
        }
    }
}