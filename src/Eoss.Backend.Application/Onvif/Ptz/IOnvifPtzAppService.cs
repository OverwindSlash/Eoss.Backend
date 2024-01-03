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

        Task<PtzStatusDto> AbsoluteMoveAsync(string host, string username, string password, string profileToken,
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed);
        Task<PtzStatusDto> RelativeMoveAsync(string host, string username, string password, string profileToken,
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed);
        Task ContinuousMoveAsync(string host, string username, string password, string profileToken,
            float panSpeed, float tiltSpeed, float zoomSpeed);
        Task<PtzStatusDto> StopAsync(string host, string username, string password, string profileToken,
            bool stopPan, bool stopZoom);
    }
}
