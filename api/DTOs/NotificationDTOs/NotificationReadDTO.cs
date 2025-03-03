using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.NotificationDTOs
{
    public class NotificationReadDTO
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string message { get; set; } = string.Empty;

        public DateTime timestamp { get; set; }
    }
}