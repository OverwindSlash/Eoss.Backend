﻿using Abp.Application.Services;
using Abp.UI;
using Eoss.Backend.Domain.Onvif;
using Eoss.Backend.Onvif.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif
{
    public class OnvifMediaAppService : ApplicationService, IOnvifMediaAppService
    {
        private readonly IOnvifMediaManager _mediaManager;

        public OnvifMediaAppService(IOnvifMediaManager mediaManager)
        {
            _mediaManager = mediaManager;
        }

        [HttpGet]
        public async Task<List<ProfileDto>> GetProfilesAsync(string host, string username, string password)
        {
            try
            {
                var profiles = await _mediaManager.GetProfilesAsync(host, username, password);
                return ObjectMapper.Map<List<ProfileDto>>(profiles);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        [HttpGet]
        public async Task<VideoSourceDto> GetVideoSourcesAsync(string host, string username, string password, string profileToken)
        {
            try
            {
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

                    return videoSourceDto;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}
