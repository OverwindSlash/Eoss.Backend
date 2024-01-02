using System.Threading.Tasks;
using Abp.Application.Services;
using Eoss.Backend.Authorization.Accounts.Dto;

namespace Eoss.Backend.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
