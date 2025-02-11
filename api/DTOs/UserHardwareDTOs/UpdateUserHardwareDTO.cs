using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace api.DTOs.UserHardwareDTOs
{
    public class UpdateUserHardwareDTO
    {
        [Required(ErrorMessage = "Start Date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}-\d{2}:\d{2}$", ErrorMessage = "Start Date must be in the format yyyy-MM-dd-HH:mm.")]
        public string startDate { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}-\d{2}:\d{2}$", ErrorMessage = "Start Date must be in the format yyyy-MM-dd-HH:mm.")]
        public string endDate { get; set; }
        public DateTime? deliveryDate { get; set; } // Nullable
        public bool isRented { get; set; }

        public DateTime GetStartDateTime()
        {
            return DateTime.ParseExact(startDate, "yyyy-MM-dd-HH:mm", CultureInfo.InvariantCulture);
        }

        public DateTime GetEndDateTime()
        {
            return DateTime.ParseExact(endDate, "yyyy-MM-dd-HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
