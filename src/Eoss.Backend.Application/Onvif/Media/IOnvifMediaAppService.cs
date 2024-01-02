using Abp.Application.Services;
using Eoss.Backend.Onvif.Media.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eoss.Backend.Onvif.Media
{
    public interface IOnvifMediaAppService : IApplicationService
    {
        Task<List<ProfileDto>> GetProfilesAsync(string host, string username, string password);
        Task<List<VideoSourceDto>> GetVideoSourcesAsync(string host, string username, string password, string profileToken);
    }
}
