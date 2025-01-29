using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.RoleDTOs;

namespace DTOs.UserDTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsVerified { get; set; }
        public int RoleId { get; set; }
        public RoleReadDTO Role { get; set; } = new RoleReadDTO();  // Use the custom RoleDTO
    }
}