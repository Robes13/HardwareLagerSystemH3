using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class HardwareCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int hardwareid { get; set; }
        public Hardware hardware { get; set; } = null!;

        [Required]
        public int categoryid { get; set; }

        public Category category { get; set; } = null!;
    }
}