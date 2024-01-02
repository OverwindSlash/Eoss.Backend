using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Eoss.Backend.Configuration;

namespace Eoss.Backend.Web.Host.Startup
{
    [DependsOn(
       typeof(BackendWebCoreModule))]
    public class BackendWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BackendWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BackendWebHostModule).GetAssembly());
        }
    }
}
