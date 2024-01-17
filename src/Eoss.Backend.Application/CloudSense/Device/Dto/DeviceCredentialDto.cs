using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Eoss.Backend.Entities;

namespace Eoss.Backend.CloudSense.Dto
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
