using System;

namespace api.DTOs.UserHardwareDTOs
{
    public class ReadUserHardwareDTO
    {
        public int id { get; set; }
        public string? fullName { get; set; }
        public string? hardware { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public DateTime? deliveryDate { get; set; } // Nullable

        public bool isRented { get; set; }
    }
}
