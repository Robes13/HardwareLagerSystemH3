using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using DTOs.TypeDTOs;

namespace DTOs.HardwareDTOs
{
    public class HardwareReadDTO
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? hardwarestatus { get; set; }
        public string? type { get; set; }
        public List<string> hardwarecategories { get; set; } = new List<string>();
        public string? ImageUrl { get; set; }

        public string Description { get; set; } = string.Empty;

    }
}