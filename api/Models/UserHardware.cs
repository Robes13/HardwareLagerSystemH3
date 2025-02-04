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
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public bool isRented { get; set; } = false;
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
