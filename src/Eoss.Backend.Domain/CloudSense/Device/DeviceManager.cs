using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Eoss.Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eoss.Backend.Domain.CloudSense
{
    public class DeviceManager : DomainService, IDeviceManager
    {
        private readonly IRepository<Device> _deviceRepository;
        private readonly IRepository<Credential> _credentialRepository;
        private readonly IRepository<InstallationParams> _installationParamsRepository;
        private readonly IRepository<Profile> _profileRepository;
        private readonly IRepository<PtzParams> _ptzParamsRepository;

        public DeviceManager(
            IRepository<Device> deviceRepository,
            IRepository<Credential> credentialRepository,
            IRepository<InstallationParams> installationParamsRepository,
            IRepository<Profile> profileRepository,
            IRepository<PtzParams> ptzParamsRepository
            )
        {
            _deviceRepository = deviceRepository;
            _credentialRepository = credentialRepository;
            _installationParamsRepository = installationParamsRepository;
            _profileRepository = profileRepository;
            _ptzParamsRepository = ptzParamsRepository;
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

        public async Task RemoveCredentialAsync(string deviceId)
        {
            await _credentialRepository.DeleteAsync(cred => cred.Device.DeviceId == deviceId);
        }

        public async Task RemoveInstallationParams(string deviceId)
        {
            var device = await _deviceRepository.GetAll()
                .Include(device => device.InstallationParams)
                .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

            if (device != null)
            {
                await _installationParamsRepository.DeleteAsync(install => install.Id == device.InstallationParams.Id);
            }
        }

        public async Task RemoveProfileAsync(string profileToken)
        {
            var profile = await _profileRepository.FirstOrDefaultAsync(profile => profile.Token == profileToken);
            if (profile != null)
            {
                await _ptzParamsRepository.DeleteAsync(ptz => ptz.Id == profile.PtzParams.Id);
            }

            await _profileRepository.DeleteAsync(profile => profile.Token == profileToken);
        }
    }
}
