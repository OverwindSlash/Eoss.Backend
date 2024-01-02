using Abp.AutoMapper;
using Eoss.Backend.Entities;
using Eoss.Backend.Onvif.Ptz.Dto;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Onvif.Media.Dto
{
    [AutoMapFrom(typeof(Profile))]
    public class ProfileDto
    {
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
        public PtzParamsDto PtzParams { get; set; }
    }
}
