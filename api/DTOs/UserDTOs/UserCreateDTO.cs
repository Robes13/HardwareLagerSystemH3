using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace DTOs.UserDTOs
{
    public class UserCreateDTO
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50, ErrorMessage = "Username is longer then 50 characters")]
        public string username { get; set;} = string.Empty;

        [Required]
        public string hashedpassword { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(100, ErrorMessage = "Email is longer then 50 characters")]
        public string email { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "Fullname is longer then 50 characters")]
        public string fullname { get; set; } = string.Empty;

    }
}