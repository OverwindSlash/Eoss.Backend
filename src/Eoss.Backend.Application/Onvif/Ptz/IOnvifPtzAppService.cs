using Abp.Application.Services;
using Eoss.Backend.Onvif.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif
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

        Task<List<PtzPresetDto>> GetPresetsAsync(string host, string username, string password, string profileToken);
        Task<PtzStatusDto> GotoPresetAsync(string host, string username, string password, string profileToken, 
            string presetToken, float panSpeed, float tiltSpeed, float zoomSpeed);
        Task<string> SetPresetAsync(string host, string username, string password, string profileToken, 
            string presetToken, string presetName);
    }
}
