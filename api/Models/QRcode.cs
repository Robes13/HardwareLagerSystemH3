using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class QRcode
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int userhardwareid { get; set; }
    }
}