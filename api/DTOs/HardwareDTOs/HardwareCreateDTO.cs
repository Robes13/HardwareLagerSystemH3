using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.HardwareDTOs
{
    public class HardwareCreateDTO
    {
        public string name { get; set; } = string.Empty;
        public string hardwarestatusid { get; set; }
        public string typeid { get; set; }
    }
}