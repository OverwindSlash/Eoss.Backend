using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.CloudSense.Device.Dto
{
    [AutoMap(typeof(Credential))]
    public class DeviceCredentialDto
    {
        [DisplayName("Username")]
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }

        [DisplayName("Device Id")]
        [Required]
        public string DeviceId { get; set; }
    }
}
