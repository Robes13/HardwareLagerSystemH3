using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace api.DTOs.RoleDTOs
{
    public class RoleReadDTO
    {
        public string RoleName { get; set; } = string.Empty;
    }
}