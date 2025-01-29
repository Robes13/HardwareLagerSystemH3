using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Category must be 3 characters")]
        [MaxLength(50, ErrorMessage = "Category must be shorter than 50 characters")]
        public string name { get; set; } = string.Empty;
    }
}