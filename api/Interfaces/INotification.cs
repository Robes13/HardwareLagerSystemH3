using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers.QueryObjects;
using api.Models;

namespace api.Interfaces
{
    public interface INotification
    {
        Task<List<Notification>> GetAllAsync(NotificationQueryObject query);
        Task<Notification?> GetByIdAsync(int id);
        Task<Notification> CreateAsync(Notification notificationModel);
        Task<Notification?> DeleteAsync(int id);
    }
}