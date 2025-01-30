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

        [Required]
        public int categoryid { get; set; }

        public Hardware hardware { get; set; } = null!;
        public Category category { get; set; } = null!;

        public List<HardwareCategory> HardwareCategories { get; set; } = new List<HardwareCategory>();

    }
}