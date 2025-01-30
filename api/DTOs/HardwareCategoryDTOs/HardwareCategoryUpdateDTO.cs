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
        public int hardwareid { get; set; }

        [Required]
        public int categoryid { get; set; }
    }
}