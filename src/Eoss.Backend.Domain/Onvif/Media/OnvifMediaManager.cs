using Abp.Domain.Services;
using Mictlanix.DotNet.Onvif;
using Mictlanix.DotNet.Onvif.Common;
using Profile = Eoss.Backend.Entities.Profile;

namespace Eoss.Backend.Domain.Onvif.Media
{
    public class OnvifMediaManager : DomainService, IOnvifMediaManager
    {
        public async Task<List<Profile>> GetProfilesAsync(string host, string username, string password)
        {
            return await DoGetProfilesAsync(host, username, password);
        }

        private async Task<List<Profile>> DoGetProfilesAsync(string host, string username, string password)
        {
            var media = await OnvifClientFactory.CreateMediaClientAsync(host, username, password);
            var response = await media.GetProfilesAsync();

            List<Profile> profiles = new ();

            foreach (var onvifProfile in response.Profiles)
            {
                var profile = new Profile()
                {
                    Name = onvifProfile.Name,
                    Token = onvifProfile.token,
                };

                // VideoSourceConfiguration
                var videoSourceConfig = onvifProfile.VideoSourceConfiguration;
                if (videoSourceConfig != null)
                {
                    profile.VideoSourceToken = videoSourceConfig.SourceToken;
                }

                // VideoEncoderConfiguration
                var videoEncoderConfig = onvifProfile.VideoEncoderConfiguration;
                if (videoEncoderConfig != null)
                {
                    profile.VideoEncoding = videoEncoderConfig.Encoding != null 
                        ? videoEncoderConfig.Encoding.ToString() 
                        : string.Empty;

                    profile.VideoWidth = videoEncoderConfig.Resolution != null
                        ? videoEncoderConfig.Resolution.Width 
                        : 0;

                    profile.VideoHeight = videoEncoderConfig.Resolution != null 
                        ? videoEncoderConfig.Resolution.Height 
                        : 0;

                    profile.FrameRate = videoEncoderConfig.RateControl != null
                        ? videoEncoderConfig.RateControl.FrameRateLimit
                        : 0;
                }

                // VideoStream
                var streamSetup = new StreamSetup()
                {
                    Stream = StreamType.RTPUnicast,
                    Transport = new Transport()
                    {
                        Protocol = TransportProtocol.RTSP,
                        Tunnel = null
                    }
                };

                var streamUri = await media.GetStreamUriAsync(streamSetup, profile.Token);
                if (streamUri != null)
                {
                    profile.StreamUri = streamUri.Uri;
                }

                // AudioSourceConfiguration
                var audioSourceConfig = onvifProfile.AudioSourceConfiguration;
                if (audioSourceConfig != null)
                {
                    profile.AudioSourceToken = audioSourceConfig.SourceToken;
                }
                

                // AudioEncoderConfiguration
                var audioEncoderConfig = onvifProfile.AudioEncoderConfiguration;
                if (audioEncoderConfig != null)
                {
                    profile.AudioEncoding = audioEncoderConfig.Encoding != null
                        ? audioEncoderConfig.Encoding.ToString()
                        : string.Empty;

                    profile.AudioBitrate = audioEncoderConfig.Bitrate;
                    profile.AudioSampleRate = audioEncoderConfig.SampleRate;
                }

                // PtzConfiguration
                var ptzConfig = onvifProfile.PTZConfiguration;
                if (ptzConfig != null)
                {
                    if (ptzConfig.PanTiltLimits != null)
                    {
                        profile.PtzParams.MinPanNormal = ptzConfig.PanTiltLimits.Range.XRange.Min;
                        profile.PtzParams.MaxPanNormal = ptzConfig.PanTiltLimits.Range.XRange.Max;
                        profile.PtzParams.MinTiltNormal = ptzConfig.PanTiltLimits.Range.YRange.Min;
                        profile.PtzParams.MaxTiltNormal = ptzConfig.PanTiltLimits.Range.YRange.Max;
                    }

                    if (ptzConfig.ZoomLimits != null)
                    {
                        profile.PtzParams.MinZoomNormal = ptzConfig.ZoomLimits.Range.XRange.Min;
                        profile.PtzParams.MaxZoomNormal = ptzConfig.ZoomLimits.Range.XRange.Max;
                    }
                }
                
                profiles.Add(profile);
            }

            return profiles;
        }
    }
}
