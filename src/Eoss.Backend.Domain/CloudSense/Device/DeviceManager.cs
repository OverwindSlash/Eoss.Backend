using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Eoss.Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eoss.Backend.Domain.CloudSense.Device
{
    public class DeviceManager : DomainService, IDeviceManager
    {
        private readonly IRepository<Credential> _credentialRepository;

        public DeviceManager(IRepository<Credential> credentialRepository)
        {
            _credentialRepository = credentialRepository;
        }

        public async Task SetCredentialAsync(Credential credential)
        {
            if (credential == null)
            {
                throw new ArgumentException("Credential is null.");
            }

            await _credentialRepository.InsertOrUpdateAsync(credential);
        }

        public async Task<Credential?> GetCredentialAsync(string deviceId)
        {
            return await _credentialRepository
                .Query(q => q.Where(cred => cred.Device.DeviceId == deviceId))
                .Include(cred => cred.Device)
                .FirstOrDefaultAsync();
        }
    }
}
