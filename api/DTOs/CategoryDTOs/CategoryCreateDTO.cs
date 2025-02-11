using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.CategoryDTOs
{
    public class CategoryCreateDTO
    {
        public string name { get; set; } = string.Empty;
    }
}