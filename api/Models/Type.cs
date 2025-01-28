using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Type
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(25)")]
        [MaxLength(255, ErrorMessage = "Name is longer then 255 characters")]
        public string name { get; set; } = string.Empty;
    }
}