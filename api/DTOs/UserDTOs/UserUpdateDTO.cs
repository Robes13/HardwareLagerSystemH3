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
        public string? username { get; set; }
        public string? hashedpassword { get; set; }
        public string? fullname { get; set; }
        public int? roleid { get; set; }
    }
}