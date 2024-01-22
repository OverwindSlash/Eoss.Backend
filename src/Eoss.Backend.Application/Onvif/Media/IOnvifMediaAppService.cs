using Abp.Application.Services;
using Eoss.Backend.Onvif.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif
{
    public interface IOnvifMediaAppService : IApplicationService
    {
        Task<List<ProfileDto>> GetProfilesAsync(string host, string username, string password);
        Task<VideoSourceDto> GetVideoSourcesAsync(string host, string username, string password, string profileToken);
    }
}
