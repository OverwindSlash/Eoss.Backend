using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Ptz
{
    public interface IOnvifPtzManager : IDomainService
    {
        Task<List<PtzConfig>> GetConfigurationsAsync(string host, string username, string password);

        Task<PtzStatus> GetStatusAsync(string host, string username, string password, string profileToken);

        Task<PtzStatus> AbsoluteMoveAsync(string host, string username, string password, string profileToken,
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed);
        Task<PtzStatus> RelativeMoveAsync(string host, string username, string password, string profileToken,
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed);
        Task ContinuousMoveAsync(string host, string username, string password, string profileToken,
            float panSpeed, float tiltSpeed, float zoomSpeed);
        Task<PtzStatus> StopAsync(string host, string username, string password, string profileToken,
            bool stopPan, bool stopZoom);
    }
}
