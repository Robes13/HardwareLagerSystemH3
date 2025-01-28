using api.Models.Enums;
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
        public int id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255, ErrorMessage = "Name is longer then 255 characters")]
        public string name { get; set; } = string.Empty;

        [Required]
        public HardwareStatus status { get; set; }

        [Required]
        public int typeid { get; set; }
        public Type type { get; set; } = null!;
    }
}