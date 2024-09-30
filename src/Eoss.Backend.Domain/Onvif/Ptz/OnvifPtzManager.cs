using Abp.Dependency;
using Eoss.Backend.Entities;
using Mictlanix.DotNet.Onvif;
using Mictlanix.DotNet.Onvif.Common;
using Mictlanix.DotNet.Onvif.Ptz;

namespace Eoss.Backend.Domain.Onvif
{
    public class OnvifPtzManager : IOnvifPtzManager, ISingletonDependency
    {
        private Dictionary<string, PTZClient> _ptzClientsCache = new();
        private Dictionary<string, PtzStatus> _lastPtzStatus = new();
        private Dictionary<string, bool> _ptzStatusNeedUpdate = new();
        
        public async Task<List<PtzConfig>> GetConfigurationsAsync(string host, string username, string password)
        {
            return await DoGetPtzConfigAsync(host, username, password);
        }

        private async Task<List<PtzConfig>> DoGetPtzConfigAsync(string host, string username, string password)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);
            var response = await ptzClient.GetConfigurationsAsync();

            List<PtzConfig> ptzConfigs = new();

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
                    if (config.PanTiltLimits.Range != null && config.PanTiltLimits.Range.XRange != null)
                    {
                        ptzConfig.PanMinLimit = config.PanTiltLimits.Range.XRange.Min;
                        ptzConfig.PanMaxLimit = config.PanTiltLimits.Range.XRange.Max;
                    }

                    if (config.PanTiltLimits.Range != null && config.PanTiltLimits.Range.YRange != null)
                    {
                        ptzConfig.TiltMinLimit = config.PanTiltLimits.Range.YRange.Min;
                        ptzConfig.TiltMaxLimit = config.PanTiltLimits.Range.YRange.Max;
                    }

                    if (config.ZoomLimits.Range != null && config.ZoomLimits.Range.XRange != null)
                    {
                        ptzConfig.ZoomMinLimit = config.ZoomLimits.Range.XRange.Min;
                        ptzConfig.ZoomMaxLimit = config.ZoomLimits.Range.XRange.Max;
                    }
                }

                ptzConfigs.Add(ptzConfig);
            }

            return ptzConfigs;
        }

        private async Task<PTZClient> GetPtzClientAsync(string host, string username, string password)
        {
            string key = $"{host},{username},{password}";
            if (_ptzClientsCache.ContainsKey(key))
            {
                return _ptzClientsCache[key];
            }
            else
            {
                var ptzClient = await OnvifClientFactory.CreatePTZClientAsync(host, username, password);
                _ptzClientsCache.Add(key, ptzClient);
                return ptzClient;
            }
        }

        public async Task<PtzStatus> GetStatusAsync(string host, string username, string password, string profileToken)
        {
            return await DoGetStatusAsync(host, username, password, profileToken);
        }

        private async Task<PtzStatus> DoGetStatusAsync(string host, string username, string password, string profileToken)
        {
            try
            {
                // if (_lastPtzStatus.ContainsKey(host) &&
                //     _ptzStatusNeedUpdate.ContainsKey(host) && _ptzStatusNeedUpdate[host] != true)
                // {
                //     Console.WriteLine($"******** Cache HIT {host} ********");
                //     return _lastPtzStatus[host];
                // }

                Console.WriteLine($"******** GetStatus {host} ********");
                var ptzClient = await GetPtzClientAsync(host, username, password);
                var response = await ptzClient.GetStatusAsync(profileToken);

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

                //if (_lastPtzStatus.ContainsKey(host))
                //{
                //    var lastStatus = _lastPtzStatus[host];
                //    if (Math.Abs(lastStatus.PanPosition - ptzStatus.PanPosition) > 0.1 ||
                //        Math.Abs(lastStatus.TiltPosition - ptzStatus.TiltPosition) > 0.1 || 
                //        Math.Abs(lastStatus.ZoomPosition - ptzStatus.ZoomPosition) > 0.1)
                //    {
                //        Console.WriteLine("******** TOO NEAR ********");
                //        _ptzStatusNeedUpdate[host] = false;
                //        return _lastPtzStatus[host];
                //    }
                //}

                if (!_lastPtzStatus.ContainsKey(host))
                {
                    _lastPtzStatus.Add(host, ptzStatus);
                }
                else
                {
                    _lastPtzStatus[host] = ptzStatus;
                }

                if (!_ptzStatusNeedUpdate.ContainsKey(host))
                {
                    _ptzStatusNeedUpdate.Add(host, false);
                }
                else
                {
                    _ptzStatusNeedUpdate[host] = false;
                }

                return ptzStatus;
            }
            catch (Exception e)
            {
                if (!_lastPtzStatus.ContainsKey(host))
                {
                    _lastPtzStatus.Add(host, new PtzStatus());
                    _ptzStatusNeedUpdate.Add(host, false);
                    
                }
                else
                {
                    _lastPtzStatus[host] = new PtzStatus();
                    _ptzStatusNeedUpdate[host] = false;
                }

                return _lastPtzStatus[host];
            }
        }

        public async Task<PtzStatus> AbsoluteMoveAsync(string host, string username, string password, string profileToken, 
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);
            await ptzClient.AbsoluteMoveAsync(profileToken, new PTZVector
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

            _ptzStatusNeedUpdate[host] = true;

            return await GetStoppedPositionAsync(profileToken, ptzClient);
        }

        private static async Task<PtzStatus> GetStoppedPositionAsync(string profileToken, PTZClient ptzClient)
        {
            try
            {
                // const int retryMaxTimes = 3;
                // int triedTime = 0;

                // while (triedTime <= retryMaxTimes)
                // {
                //     var response = await ptzClient.GetStatusAsync(profileToken);
                //     if (response.MoveStatus.PanTilt != MoveStatus.IDLE || response.MoveStatus.Zoom != MoveStatus.IDLE)
                //     {
                //         triedTime++;
                //         Thread.Sleep(30);
                //         continue;
                //     }
                //
                //     var status = new PtzStatus()
                //     {
                //         PanPosition = response.Position.PanTilt.x,
                //         TiltPosition = response.Position.PanTilt.y,
                //         ZoomPosition = response.Position.Zoom.x,
                //         PanTiltStatus = response.MoveStatus.PanTilt.ToString(),
                //         ZoomStatus = response.MoveStatus.Zoom.ToString(),
                //
                //         PanTiltSpace = response.Position.PanTilt.space,
                //         ZoomSpace = response.Position.Zoom.space,
                //
                //         UtcDateTime = response.UtcTime,
                //         Error = response.Error
                //     };
                //
                //     return status;
                // }
                
                // return new PtzStatus();
                
                var response = await ptzClient.GetStatusAsync(profileToken);

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
            catch (Exception e)
            {
                return new PtzStatus();
            }
        }

        public async Task<PtzStatus> RelativeMoveAsync(string host, string username, string password, string profileToken, 
            float pan, float tilt, float zoom, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);
            await ptzClient.RelativeMoveAsync(profileToken, new PTZVector
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
            
            _ptzStatusNeedUpdate[host] = true;

            return await GetStoppedPositionAsync(profileToken, ptzClient);
        }

        public async Task ContinuousMoveAsync(string host, string username, string password, string profileToken, 
            float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            //var stopwatch = new Stopwatch();
            var ptzClient = await GetPtzClientAsync(host, username, password);
            //Trace.WriteLine("***** Manager Phase1:" + stopwatch.ElapsedMilliseconds.ToString());
            
            //stopwatch.Restart();
            await ptzClient.ContinuousMoveAsync(profileToken, new PTZSpeed
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
            //Trace.WriteLine("***** Manager Phase2:" + stopwatch.ElapsedMilliseconds.ToString());
            
            _ptzStatusNeedUpdate[host] = true;
        }

        public async Task<PtzStatus> StopAsync(string host, string username, string password, string profileToken, bool stopPan, bool stopZoom)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);
            await ptzClient.StopAsync(profileToken, stopPan, stopZoom);
            
            _ptzStatusNeedUpdate[host] = true;

            return await GetStoppedPositionAsync(profileToken, ptzClient);
        }

        public async Task<List<PtzPreset>> GetPresetsAsync(string host, string username, string password, string profileToken)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);
            var response = await ptzClient.GetPresetsAsync(profileToken);

            List<PtzPreset> presets = response.Preset.Select(preset => new PtzPreset()
                {
                    Name = preset.Name,
                    Token = preset.token,
                    PanPosition = preset.PTZPosition.PanTilt.x,
                    TiltPosition = preset.PTZPosition.PanTilt.y,
                    PanTiltSpace = preset.PTZPosition.PanTilt.space,
                    ZoomPosition = preset.PTZPosition.Zoom.x,
                    ZoomSpace = preset.PTZPosition.Zoom.space
                }).ToList();

            return presets;
        }

        public async Task<PtzStatus> GotoPresetAsync(string host, string username, string password, string profileToken, 
            string presetToken, float panSpeed, float tiltSpeed, float zoomSpeed)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);
            await ptzClient.GotoPresetAsync(profileToken, presetToken, new PTZSpeed
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
            
            _ptzStatusNeedUpdate[host] = true;

            return await GetStoppedPositionAsync(profileToken, ptzClient);
        }

        public async Task<string> SetPresetAsync(string host, string username, string password, string profileToken, string presetToken,
            string presetName)
        {
            var ptzClient = await GetPtzClientAsync(host, username, password);

            var newPreset = await ptzClient.SetPresetAsync(new SetPresetRequest(profileToken, presetName, presetToken));

            return newPreset.PresetToken;
        }
    }
}
