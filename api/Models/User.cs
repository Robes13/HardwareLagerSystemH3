using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50, ErrorMessage = "Username is longer then 50 characters")]
        public string username { get; set; } = string.Empty;

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

        public bool isVerified { get; set; } = false;

        [Required]
        public int roleid { get; set; }

        public Role Role { get; set; } = null!;
        public bool isdeleted { get; set; } = false;

        public DateTime? datedeleted { get; set; } = null;
    }
}