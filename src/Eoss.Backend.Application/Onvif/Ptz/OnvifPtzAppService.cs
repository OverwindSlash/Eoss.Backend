using Abp.Application.Services;
using Eoss.Backend.Domain.Onvif.Ptz;
using Eoss.Backend.Onvif.Ptz.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Ptz
{
    public class OnvifPtzAppService : ApplicationService, IOnvifPtzAppService
    {
        private readonly IOnvifPtzManager _ptzManager;

        public OnvifPtzAppService(IOnvifPtzManager ptzManager)
        {
            _ptzManager = ptzManager;
        }

        [HttpGet]
        public async Task<List<PtzConfigDto>> GetConfigurationsAsync(string host, string username, string password)
        {
            var ptzConfigs = await _ptzManager.GetConfigurationsAsync(host, username, password);
            return ObjectMapper.Map<List<PtzConfigDto>>(ptzConfigs);
        }

        public async Task<PtzStatusDto> GetStatusAsync(string host, string username, string password, string profileToken)
        {
            var ptzStatus = await _ptzManager.GetStatusAsync(host,username,password,profileToken);
            return ObjectMapper.Map<PtzStatusDto>(ptzStatus);
        }
    }
}
