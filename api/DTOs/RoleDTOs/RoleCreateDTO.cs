using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.RoleDTOs
{
    public class RoleCreateDTO
    {
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        [MaxLength(15, ErrorMessage = "Name is longer then 15 characters")]
        public string name { get; set; } = string.Empty;
    }
}