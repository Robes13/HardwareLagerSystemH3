using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.NotificationDTOs
{
    public class NotificationCreateDTO
    {
        public int userid { get; set; }
        public string message { get; set; } = string.Empty;
    }
}