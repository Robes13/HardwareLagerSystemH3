using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.UserHardwareDTOs
{
    public class CreateUserHardwareDTO
    {
        public int UserId { get; set; }
        public int HardwareId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}