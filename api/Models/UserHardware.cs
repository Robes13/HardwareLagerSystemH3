using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class UserHardware
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int userid { get; set; }
        public User User { get; set; }

        [Required]
        public int hardwareid { get; set; }
        public Hardware Hardware { get; set; }

        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime endDate { get; set; }

        public DateTime? deliveryDate { get; set; } // Nullable, auto-assigned

        public bool isRented { get; set; } = false;
    }
}
