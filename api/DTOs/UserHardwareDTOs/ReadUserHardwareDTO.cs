using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.UserHardwareDTOs
{
    public class ReadUserHardwareDTO
    {
        public int id { get; set; }
        public string? fullName { get; set; }
        public string? hardware { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public bool isRented { get; set; }
    }
}