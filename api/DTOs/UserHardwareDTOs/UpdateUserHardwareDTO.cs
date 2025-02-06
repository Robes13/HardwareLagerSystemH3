using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs.UserHardwareDTOs
{
    public class UpdateUserHardwareDTO
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public bool isRented { get; set; }
    }
}