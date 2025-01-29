using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.HardwareCategoryDTOs
{
    public class HardwareCategoryUpdateDTO
    {
        [Required]
        [Range(0, 500000)]
        public int hardwareid { get; set; }

        [Required]
        [Range(0, 500000)]
        public int categoryid { get; set; }
    }
}