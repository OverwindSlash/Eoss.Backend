using Abp.Domain.Services;
using Eoss.Backend.Entities;
using Mictlanix.DotNet.Onvif;
using Mictlanix.DotNet.Onvif.Common;
using Mictlanix.DotNet.Onvif.Ptz;

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

        public async Task<PtzStatus> AbsoluteMoveAsync(string host, string username, string password, string profileToken, 
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptz = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
            await ptz.AbsoluteMoveAsync(profileToken, new PTZVector
            {
                PanTilt = new Vector2D
                {
                    x = pan,
                    y = tilt
                },
                Zoom = new Vector1D
                {
                    x = zoom
                }
            }, new PTZSpeed
            {
                PanTilt = new Vector2D
                {
                    x = panSpeed,
                    y = tiltSpeed
                },
                Zoom = new Vector1D
                {
                    x = zoomSpeed
                }
            });

            return await GetStoppedPositionAsync(profileToken, ptz);
        }

        public async Task<PtzStatus> RelativeMoveAsync(string host, string username, string password, string profileToken, float pan, float tilt,
            float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptz = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
            await ptz.RelativeMoveAsync(profileToken, new PTZVector
            {
                PanTilt = new Vector2D
                {
                    x = pan,
                    y = tilt
                },
                Zoom = new Vector1D
                {
                    x = zoom
                }
            }, new PTZSpeed
            {
                PanTilt = new Vector2D
                {
                    x = panSpeed,
                    y = tiltSpeed
                },
                Zoom = new Vector1D
                {
                    x = zoomSpeed
                }
            });

            return await GetStoppedPositionAsync(profileToken, ptz);
        }

        public async Task ContinuousMoveAsync(string host, string username, string password, string profileToken, float panSpeed,
            float tiltSpeed, float zoomSpeed)
        {
            var ptz = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
            await ptz.ContinuousMoveAsync(profileToken, new PTZSpeed
            {
                PanTilt = new Vector2D
                {
                    x = panSpeed,
                    y = tiltSpeed
                },
                Zoom = new Vector1D
                {
                    x = zoomSpeed
                }

            }, null);
        }

        public async Task<PtzStatus> StopAsync(string host, string username, string password, string profileToken, bool stopPan, bool stopZoom)
        {
            var ptz = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
            await ptz.StopAsync(profileToken, stopPan, stopZoom);

            return await GetStoppedPositionAsync(profileToken, ptz);
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

        private static async Task<PtzStatus> GetStoppedPositionAsync(string profileToken, PTZClient ptz)
        {
            try
            {
                while (true)
                {
                    var response = await ptz.GetStatusAsync(profileToken);
                    if ((response.MoveStatus.PanTilt != MoveStatus.IDLE) || (response.MoveStatus.Zoom != MoveStatus.IDLE))
                    {
                        continue;
                    }

                    var status = new PtzStatus()
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

                    return status;
                }
            }
            catch (Exception e)
            {
                return new PtzStatus();
            }
        }
    }
}
