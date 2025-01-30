using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.HardwareStatusDTOs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class HardwareStatusRepository : IHardwareStatus
    {
        private readonly ApiDbContext _context;
        public HardwareStatusRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<HardwareStatus> CreateHardwareStatusAsync(HardwareStatus hardwareStatus)
        {
            await _context.HardwareStatus.AddAsync(hardwareStatus);
            await _context.SaveChangesAsync();
            return hardwareStatus;
        }

        public async Task<HardwareStatus?> DeleteHardwareStatusAsync(int id)
        {
            var hardwareStatusToDelete = _context.HardwareStatus.Find(id);
            if(hardwareStatusToDelete == null)
            {
                return null;
            }
            _context.HardwareStatus.Remove(hardwareStatusToDelete);
            await _context.SaveChangesAsync();
            return hardwareStatusToDelete;
        }

        public Task<List<HardwareStatus>> GetAllHardwareStatusesAsync()
        {
            return _context.HardwareStatus.ToListAsync();
        }

        public async Task<HardwareStatus?> GetHardwareStatusById(int id)
        {
            return await _context.HardwareStatus.FindAsync(id);
        }

        public async Task<HardwareStatus?> UpdateHardwareStatusAsync(int id, HardwareStatusUpdateDTO hardwareStatus)
        {
            var existingHardwareStatus = await _context.HardwareStatus.FindAsync(id);
            if(existingHardwareStatus == null)
            {
                return null;
            }
            existingHardwareStatus.name = hardwareStatus.name;
            await _context.SaveChangesAsync();
            return existingHardwareStatus;
        }
    }
}