using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.Onvif
{
    public interface IOnvifMediaManager : IDomainService
    {
        Task<List<Profile>> GetProfilesAsync(string host, string username, string password);
    }
}
