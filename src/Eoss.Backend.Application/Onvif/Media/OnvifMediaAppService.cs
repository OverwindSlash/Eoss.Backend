using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eoss.Backend.Domain.Onvif.Media;
using Eoss.Backend.Onvif.Media.Dto;

namespace Eoss.Backend.Onvif.Media
{
    public class OnvifMediaAppService : ApplicationService, IOnvifMediaAppService
    {
        private readonly IOnvifMediaManager _mediaManager;

        public OnvifMediaAppService(IOnvifMediaManager mediaManager)
        {
            _mediaManager = mediaManager;
        }

        public async Task<List<ProfileDto>> GetProfilesAsync(string host, string username, string password)
        {
            var profiles = await _mediaManager.GetProfilesAsync(host, username, password);
            return ObjectMapper.Map<List<ProfileDto>>(profiles);
        }

        public async Task<List<VideoSourceDto>> GetVideoSourcesAsync(string host, string username, string password, string profileToken)
        {
            List<VideoSourceDto> videoSourceDtos = new();

            var profiles = await _mediaManager.GetProfilesAsync(host, username, password);
            var profile = profiles.SingleOrDefault(p => p.Token == profileToken);
            if (profile != null)
            {
                var videoSourceDto = new VideoSourceDto()
                {
                    Token = profile.VideoSourceToken,
                    StreamUri = profile.StreamUri,
                    VideoWidth = profile.VideoWidth,
                    VideoHeight = profile.VideoHeight,
                    Framerate = profile.FrameRate
                };

                videoSourceDtos.Add(videoSourceDto);
            }

            return videoSourceDtos;
        }
    }
}
