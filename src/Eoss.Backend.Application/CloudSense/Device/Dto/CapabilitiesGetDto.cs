﻿using System.Collections.Generic;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Eoss.Backend.Entities;

namespace Eoss.Backend.CloudSense.Dto
{
    [AutoMapFrom(typeof(Capabilities))]
    public class CapabilitiesGetDto : EntityDto<int>
    {
        [DisplayName("Analytics Address")]
        public string AnalyticsXAddress { get; set; } = string.Empty;

        [DisplayName("Device Address")]
        public string DeviceXAddress { get; set; } = string.Empty;

        [DisplayName("Event Address")]
        public string EventXAddress { get; set; } = string.Empty;

        [DisplayName("Imaging Address")]
        public string ImagingXAddress { get; set; } = string.Empty;

        [DisplayName("Media Address")]
        public string MediaXAddress { get; set; } = string.Empty;

        [DisplayName("PTZ Address")]
        public string PtzXAddress { get; set; } = string.Empty;

        [DisplayName("Supported Onvif Versions")]
        public List<string> SupportedOnvifVersions { get; set; } = new List<string>();
    }
}
