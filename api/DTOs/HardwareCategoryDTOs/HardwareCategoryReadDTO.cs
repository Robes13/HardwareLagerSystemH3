using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.HardwareCategoryDTOs
{
    public class HardwareCategoryReadDTO
    {
        public int id { get; set; }
        public string? hardware { get; set; }
        public string? category { get; set; }
    }
}