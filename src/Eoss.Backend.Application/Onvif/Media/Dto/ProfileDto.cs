using Abp.AutoMapper;
using Eoss.Backend.Entities;
using System.ComponentModel;

namespace Eoss.Backend.Onvif.Dto
{
    [AutoMapFrom(typeof(Profile))]
    public class ProfileDto
    {
        [DisplayName("Profile name")]
        public string Name { get; set; }

        [DisplayName("Profile token")]
        public string Token { get; set; }

        [DisplayName("Video source token")]
        public string VideoSourceToken { get; set; }

        [DisplayName("Video source encoding")]
        public string VideoEncoding { get; set; }

        [DisplayName("Video width")]
        public int VideoWidth { get; set; }

        [DisplayName("Video height")]
        public int VideoHeight { get; set; }

        [DisplayName("Frame rate")]
        public float FrameRate { get; set; }

        [DisplayName("Video Stream Uri")]
        public string StreamUri { get; set; }

        [DisplayName("Audio source token")]
        public string AudioSourceToken { get; set; }

        [DisplayName("Audio source encoding")]
        public string AudioEncoding { get; set; }

        [DisplayName("Audio Bitrate")]
        public int AudioBitrate { get; set; }

        [DisplayName("Audio sample rate")]
        public int AudioSampleRate { get; set; }

        [DisplayName("PTZ parameters")]
        public PtzParamsGetDto PtzParams { get; set; }
    }
}
