using Abp.Application.Services;
using Eoss.Backend.CloudSense.Dto;
using Eoss.Backend.Onvif.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.CloudSense
{
    public interface IPtzAppService : IApplicationService
    {
        Task SetPtzParamsAsync(string deviceId, string profileToken, PtzParamsSaveDto input);
        Task<PtzParamsGetDto> GetPtzParamsAsync(string deviceId, string profileToken);

        Task<PtzStatusDto> GetStatusAsync(string deviceId, string profileToken);
        Task<PtzStatusDto> AbsoluteMoveAsync(string deviceId, string profileToken, float pan, float tilt, float zoom,
            float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1);
        Task<PtzStatusDto> RelativeMoveAsync(string deviceId, string profileToken, float pan, float tilt, float zoom,
            float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1);

        Task<PtzStatusInDegreeDto> GetStatusInDegreeAsync(string deviceId, string profileToken);
        Task<PtzStatusInDegreeDto> AbsoluteMoveWithDegreeAsync(string deviceId, string profileToken,
            float panInDegree, float tiltInDegree, float zoomInLevel, float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1);
        Task<PtzStatusInDegreeDto> RelativeMoveWithDegreeAsync(string deviceId, string profileToken,
            float panInDegree, float tiltInDegree, float zoomInLevel, float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1);

        Task ContinuousMoveAsync(string deviceId, string profileToken, float panSpeed, float tiltSpeed, float zoomSpeed);
        Task<PtzStatusDto> StopAsync(string deviceId, string profileToken, bool stopPan, bool stopZoom);

        Task<List<PtzPresetDto>> GetPresetsAsync(string deviceId, string profileToken);
        Task<PtzStatusDto> GotoPresetAsync(string deviceId, string profileToken, string presetToken, 
            float panSpeed = 1, float tiltSpeed = 1, float zoomSpeed = 1);
        Task<string> SetPresetAsync(string deviceId, string profileToken, string presetToken, string presetName);
    }
}
