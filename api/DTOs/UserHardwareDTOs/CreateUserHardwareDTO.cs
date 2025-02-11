using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace api.DTOs.UserHardwareDTOs
{
    public class CreateUserHardwareDTO
    {
        public int UserId { get; set; }
        public int HardwareId { get; set; }

        // Store Date as string in format "yyyy-MM-dd-HH:mm" and convert to DateTime

        [Required(ErrorMessage = "Start Date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}-\d{2}:\d{2}$", ErrorMessage = "Start Date must be in the format yyyy-MM-dd-HH:mm.")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}-\d{2}:\d{2}$", ErrorMessage = "End Date must be in the format yyyy-MM-dd-HH:mm.")]
        public string EndDate { get; set; }

        public DateTime GetStartDateTime()
        {
            return DateTime.ParseExact(StartDate, "yyyy-MM-dd-HH:mm", CultureInfo.InvariantCulture);
        }

        public DateTime GetEndDateTime()
        {
            return DateTime.ParseExact(EndDate, "yyyy-MM-dd-HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
