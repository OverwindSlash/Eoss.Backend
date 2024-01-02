using System.Threading.Tasks;
using Abp.Application.Services;
using Eoss.Backend.Sessions.Dto;

namespace Eoss.Backend.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
