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
        public string name { get; set; } = string.Empty;
    }
}