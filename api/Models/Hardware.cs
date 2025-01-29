using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Hardware
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255, ErrorMessage = "Name is longer then 255 characters")]
        public string name { get; set; } = string.Empty;

        [Required]
        public int hardwarestatusid { get; set; }
        public HardwareStatus hardwarestatus = null!;

        [Required]
        public int typeid { get; set; }
        public Types type { get; set; } = null!;
    }
}