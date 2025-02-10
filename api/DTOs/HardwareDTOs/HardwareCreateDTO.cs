using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.HardwareDTOs
{
    public class HardwareCreateDTO
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Name is longer then 255 characters")]
        public string name { get; set; } = string.Empty;

        [Required]
        public string hardwarestatusid { get; set; }

        [Required]
        public string typeid { get; set; }
    }
}