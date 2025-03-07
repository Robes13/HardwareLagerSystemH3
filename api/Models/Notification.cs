using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int userid { get; set; }
        public User user { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255, ErrorMessage = "Message is longer than 255 characters")]
        public string message { get; set; } = string.Empty;
        public DateTime timestamp { get; set; } = DateTime.UtcNow;
    }
}