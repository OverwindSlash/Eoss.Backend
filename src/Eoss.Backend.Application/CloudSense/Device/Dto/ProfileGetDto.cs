using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Eoss.Backend.Entities;

namespace Eoss.Backend.CloudSense.Dto
{
    [AutoMapFrom(typeof(Profile))]
    public class ProfileGetDto
    {
        [DisplayName("Profile id")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Profile name")]
        [Required, StringLength(BackendConsts.MaxNameLength, MinimumLength = BackendConsts.MinNameLength)]
        public string Name { get; set; }

        [DisplayName("Profile token")]
        [Required, StringLength(BackendConsts.MaxStringIdLength, MinimumLength = BackendConsts.MinStringIdLength)]
        public string Token { get; set; }

        [DisplayName("Video source token")]
        [Required, StringLength(BackendConsts.MaxStringIdLength, MinimumLength = BackendConsts.MinStringIdLength)]
        public string VideoSourceToken { get; set; }

        [DisplayName("Video source encoding")]
        [Required]
        public string VideoEncoding { get; set; }

        [DisplayName("Video width")]
        [Required, Range(1, int.MaxValue)]
        public int VideoWidth { get; set; }

        [DisplayName("Video height")]
        [Required, Range(1, int.MaxValue)]
        public int VideoHeight { get; set; }

        [DisplayName("Frame rate")]
        [Range(0, float.MaxValue)]
        public float FrameRate { get; set; }

        [DisplayName("Video Stream Uri")]
        [Required]
        public string StreamUri { get; set; }

        [DisplayName("Audio source token")]
        [Required, StringLength(BackendConsts.MaxStringIdLength, MinimumLength = BackendConsts.MinStringIdLength)]
        public string AudioSourceToken { get; set; }

        [DisplayName("Audio source encoding")]
        [Required]
        public string AudioEncoding { get; set; }

        [DisplayName("Audio Bitrate")]
        [Required, Range(1, int.MaxValue)]
        public int AudioBitrate { get; set; }

        [DisplayName("Audio sample rate")]
        [Required, Range(1, int.MaxValue)]
        public int AudioSampleRate { get; set; }

        [DisplayName("PTZ parameters")]
        public PtzParams PtzParams { get; set; } = new PtzParams();

        public string DeviceId { get; set; }
    }
}
