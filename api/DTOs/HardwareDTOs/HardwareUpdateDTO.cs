using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.HardwareDTOs
{
    public class HardwareUpdateDTO
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Description is longer then 255 characters")]
        public string name { get; set; } = string.Empty;

        [Required]
        public int hardwarestatusid { get; set; }

        [Required]
        public int typeid { get; set; }
    }
}