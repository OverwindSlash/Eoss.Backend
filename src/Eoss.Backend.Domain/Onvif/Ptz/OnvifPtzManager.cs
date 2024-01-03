using Abp.Domain.Services;
using Eoss.Backend.Entities;
using Mictlanix.DotNet.Onvif;

namespace Eoss.Backend.Domain.Onvif.Ptz
{
    public class OnvifPtzManager : DomainService, IOnvifPtzManager
    {
        public async Task<List<PtzConfig>> GetConfigurationsAsync(string host, string username, string password)
        {
            return await DoGetPtzConfigAsync(host, username, password);
        }

        public async Task<PtzStatus> GetStatusAsync(string host, string username, string password, string profileToken)
        {
            return await DoGetStatusAsync(host, username, password, profileToken);
        }

        private static async Task<List<PtzConfig>> DoGetPtzConfigAsync(string host, string username, string password)
        {
            var ptz = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
            var response = await ptz.GetConfigurationsAsync();

            List<PtzConfig> ptzConfigs = new ();

            foreach (var config in response.PTZConfiguration)
            {
                var ptzConfig = new PtzConfig()
                {
                    NodeToken = config.NodeToken,
                    DefaultAbsolutePantTiltPositionSpace = config.DefaultAbsolutePantTiltPositionSpace,
                    DefaultAbsoluteZoomPositionSpace = config.DefaultAbsoluteZoomPositionSpace,
                    DefaultRelativePanTiltTranslationSpace = config.DefaultRelativePanTiltTranslationSpace,
                    DefaultRelativeZoomTranslationSpace = config.DefaultRelativeZoomTranslationSpace,
                    DefaultContinuousPanTiltVelocitySpace = config.DefaultContinuousPanTiltVelocitySpace,
                    DefaultContinuousZoomVelocitySpace = config.DefaultContinuousZoomVelocitySpace,
                    
                    DefaultPTZTimeout = config.DefaultPTZTimeout,

                    MoveRamp = config.MoveRamp,
                    MoveRampSpecified = config.MoveRampSpecified,
                    PresetRamp = config.PresetRamp,
                    PresetRampSpecified = config.PresetRampSpecified,
                    PresetTourRamp = config.PresetTourRamp,
                    PresetTourRampSpecified = config.PresetTourRampSpecified
                };

                // PTZ speed
                if (config.DefaultPTZSpeed != null)
                {
                    if (config.DefaultPTZSpeed.PanTilt != null)
                    {
                        ptzConfig.DefaultPanSpeed = config.DefaultPTZSpeed.PanTilt.x;
                        ptzConfig.DefaultTiltSpeed = config.DefaultPTZSpeed.PanTilt.y;
                        ptzConfig.DefaultPanTiltSpace = config.DefaultPTZSpeed.PanTilt.space;
                    }

                    if (config.DefaultPTZSpeed.Zoom != null)
                    {
                        ptzConfig.DefaultZoomSpeed = config.DefaultPTZSpeed.Zoom.x;
                        ptzConfig.DefaultZoomSpace = config.DefaultPTZSpeed.Zoom.space;
                    }
                }

                // PTZ limit
                if (config.PanTiltLimits != null)
                {
                    if ((config.PanTiltLimits.Range != null) && (config.PanTiltLimits.Range.XRange != null))
                    {
                        ptzConfig.PanMinLimit = config.PanTiltLimits.Range.XRange.Min;
                        ptzConfig.PanMaxLimit = config.PanTiltLimits.Range.XRange.Max;
                    }

                    if ((config.PanTiltLimits.Range != null) && (config.PanTiltLimits.Range.YRange != null))
                    {
                        ptzConfig.TiltMinLimit = config.PanTiltLimits.Range.YRange.Min;
                        ptzConfig.TiltMaxLimit = config.PanTiltLimits.Range.YRange.Max;
                    }

                    if ((config.ZoomLimits.Range != null) && (config.ZoomLimits.Range.XRange != null))
                    {
                        ptzConfig.ZoomMinLimit = config.ZoomLimits.Range.XRange.Min;
                        ptzConfig.ZoomMaxLimit = config.ZoomLimits.Range.XRange.Max;
                    }
                }

                ptzConfigs.Add(ptzConfig);
            }

            return ptzConfigs;
        }

        private static async Task<PtzStatus> DoGetStatusAsync(string host, string username, string password, string profileToken)
        {
            var ptz = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
            var response = await ptz.GetStatusAsync(profileToken);

            var ptzStatus = new PtzStatus()
            {
                PanPosition = response.Position.PanTilt.x,
                TiltPosition = response.Position.PanTilt.y,
                ZoomPosition = response.Position.Zoom.x,

                PanTiltStatus = response.MoveStatus.PanTilt.ToString(),
                ZoomStatus = response.MoveStatus.Zoom.ToString(),

                PanTiltSpace = response.Position.PanTilt.space,
                ZoomSpace = response.Position.Zoom.space,

                UtcDateTime = response.UtcTime,
                Error = response.Error
            };

            return ptzStatus;
        }
    }
}
