using Abp.Application.Services;
using Eoss.Backend.Onvif.Ptz.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Ptz
{
    public interface IOnvifPtzAppService : IApplicationService
    {
        Task<List<PtzConfigDto>> GetConfigurationsAsync(string host, string username, string password);

        Task<PtzStatusDto> GetStatusAsync(string host, string username, string password, string profileToken);
    }
}
