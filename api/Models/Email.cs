using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Email
    {
        #region Columns

        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(255, ErrorMessage = "Email is longer then 255 characters")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string SecretKey { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "bit")]
        public bool IsVerified { get; set; } = false;
        #endregion
    }
}