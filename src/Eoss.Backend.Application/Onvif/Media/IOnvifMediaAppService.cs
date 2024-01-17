using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eoss.Backend.Onvif.Dto;

namespace Eoss.Backend.Onvif
{
    public interface IOnvifMediaAppService : IApplicationService
    {
        Task<List<ProfileDto>> GetProfilesAsync(string host, string username, string password);
        Task<List<VideoSourceDto>> GetVideoSourcesAsync(string host, string username, string password, string profileToken);
    }
}
