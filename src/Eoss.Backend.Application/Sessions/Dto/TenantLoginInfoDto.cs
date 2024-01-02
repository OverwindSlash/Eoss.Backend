using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eoss.Backend.MultiTenancy;

namespace Eoss.Backend.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
