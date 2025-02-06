using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.NotificationDTOs;
using api.Models;
using DTOs.NotificationDTOs;

namespace Mappers
{
    public static class NotificationMapper
    {
        public static NotificationReadDTO ToNotificationDto(this Notification notificationModel)
        {
            return new NotificationReadDTO
            {
                id = notificationModel.id,
                userhardwareid = notificationModel.userhardwareid,
                message = notificationModel.message,
            };
        }
        public static Notification ToNotificationFromCreate(this NotificationCreateDTO notificationDto)
        {
            return new Notification
            {
                userhardwareid = notificationDto.userhardwareid,
                message = notificationDto.message
            };
        }
    }
}