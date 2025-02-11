using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.HardwareDTOs
{
    public class HardwareUpdateDTO
    {
        public string name { get; set; } = string.Empty;

        public int hardwarestatusid { get; set; }
        public int typeid { get; set; }
    }
}