using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        [MaxLength(15, ErrorMessage = "Name is longer then 15 characters")]
        public string name { get; set; } = string.Empty;
    }
}
