using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.HardwareCategoryDTOs
{
    public class HardwareCategoryUpdateDTO
    {
        public int hardwareid { get; set; }
        public int categoryid { get; set; }
    }
}