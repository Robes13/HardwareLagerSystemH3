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
        public User user { get; set; } = null!;


        [Required]
        public int hardwareid { get; set; }
        public Hardware hardware { get; set; } = null!; 

        [Required]
        public int categoryid { get; set; }
        public Category category { get; set; } = null!;

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
