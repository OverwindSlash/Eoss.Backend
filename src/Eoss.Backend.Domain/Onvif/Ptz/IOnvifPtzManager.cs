using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Ptz
{
    public interface IOnvifPtzManager : IDomainService
    {
        Task<List<PtzConfig>> GetConfigurationsAsync(string host, string username, string password);

        Task<PtzStatus> GetStatusAsync(string host, string username, string password, string profileToken);
    }
}
