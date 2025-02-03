using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.EmailDTOs;
using api.DTOs.RoleDTOs;
using api.Models;

namespace DTOs.UserDTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public RoleReadDTO Role { get; set; } = new RoleReadDTO();  // Use the custom RoleDTO
        public EmailDTO Email { get; set; } = new EmailDTO();
    }
}