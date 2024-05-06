using Abp.Domain.Services;
using Eoss.Backend.Entities;

namespace Eoss.Backend.Domain.CloudSense
{
    public interface IDeviceManager : IDomainService
    {
        Task SetCredentialAsync(Credential credential);
        Task<Credential?> GetCredentialAsync(string deviceId);
        Task RemoveCredentialAsync(string deviceId);

        Task RemoveInstallationParams(string deviceId);

        Task RemoveProfileAsync(string profileToken);
    }
}
