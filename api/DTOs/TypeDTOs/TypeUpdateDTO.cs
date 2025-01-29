using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.TypeDTOs
{
    public class TypeUpdateDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Type must be 3 characters")]
        [MaxLength(50, ErrorMessage = "Type must be shorter than 50 characters")]
        public string name { get; set; } = string.Empty;
    }
}