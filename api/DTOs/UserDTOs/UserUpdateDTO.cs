using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public string username { get; set; } = string.Empty;
        public string hashedpassword { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;
        public int roleid { get; set; }
    }
}