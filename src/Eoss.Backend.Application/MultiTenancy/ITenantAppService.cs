using Abp.Application.Services;
using Eoss.Backend.MultiTenancy.Dto;

namespace Eoss.Backend.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

