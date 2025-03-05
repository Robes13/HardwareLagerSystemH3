using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.UserHardwareDTOs
{
    public class ScanUserHardwareDTO
    {
        public string? hardwareName { get; set; } = string.Empty;
        public string? hardwareDescription { get; set; } = string.Empty;
        public string? hardwareStatus { get; set; } = string.Empty;
        public string? hardwareType { get; set; } = string.Empty;
        public List<string>? hardwareCategory { get; set; }
        public bool isUserRenter { get; set; }
        public bool isHardwareAvailable { get; set; }
    }
}