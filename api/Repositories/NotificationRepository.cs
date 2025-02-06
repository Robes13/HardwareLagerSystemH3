using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers.QueryObjects;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class NotificationRepository : INotification
    {
        private readonly ApiDbContext _context;
        public NotificationRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Notification> CreateAsync(Notification notificationModel)
        {
            await _context.Notification.AddAsync(notificationModel);
            await _context.SaveChangesAsync();
            return notificationModel;
        }

        public async Task<Notification?> DeleteAsync(int id)
        {
            var notificationModel = await _context.Notification.FirstOrDefaultAsync(x => x.id == id);

            if (notificationModel == null)
            {
                return null;
            }

            _context.Notification.Remove(notificationModel);
            await _context.SaveChangesAsync();

            return notificationModel;
        }

        public async Task<List<Notification>> GetAllAsync(NotificationQueryObject query)
        {
            var notifications = _context.Notification.AsQueryable();

            if (query.userhardwareid.HasValue)
            {
                notifications = notifications.Where(c => c.userhardwareid == query.userhardwareid);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await notifications.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notification.FindAsync(id);
        }
    }
}