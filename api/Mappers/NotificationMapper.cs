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
                userid = notificationModel.userid,
                message = notificationModel.message,
            };
        }
        public static Notification ToNotificationFromCreate(this NotificationCreateDTO notificationDto)
        {
            return new Notification
            {
                userid = notificationDto.userid,
                message = notificationDto.message
            };
        }
    }
}