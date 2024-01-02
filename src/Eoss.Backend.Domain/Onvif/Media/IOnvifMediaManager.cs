using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif.Media
{
    public interface IOnvifMediaManager : IDomainService
    {
        Task<List<Profile>> GetProfilesAsync(string host, string username, string password);
    }
}
