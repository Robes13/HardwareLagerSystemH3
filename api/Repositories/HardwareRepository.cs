using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.HardwareDTOs;
using api.Helpers.QueryObjects;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Mysqlx;

namespace api.Repositories
{
    public class HardwareRepository : IHardware
    {
        private readonly ApiDbContext _context;
        private readonly CloudinaryService _cloudinaryService; // Inject CloudinaryService

        public HardwareRepository(ApiDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;  // Initialize CloudinaryService
        }

        public async Task<Hardware> CreateAsync(Hardware hardwareModel, IFormFile? imageFile = null)
        {
            // Upload image to Cloudinary if provided
            if (imageFile != null)
            {
                hardwareModel.ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }

            // Save the hardware to the database
            await _context.Hardware.AddAsync(hardwareModel);
            await _context.SaveChangesAsync();
            return hardwareModel;
        }

        public async Task<Hardware?> DeleteAsync(int id)
        {
            var hardwareModel = await _context.Hardware.FirstOrDefaultAsync(x => x.id == id);

            if (hardwareModel == null)
            {
                return null;
            }

            _context.Hardware.Remove(hardwareModel);
            await _context.SaveChangesAsync();

            return hardwareModel;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Hardware.AnyAsync(s => s.id == id);
        }

        public async Task<List<Hardware>> GetAllAsync(HardwareQueryObject query)
        {
            var hardwares = _context.Hardware
                .Include(h => h.hardwarestatus)
                .Include(h => h.type)
                .Include(h => h.HardwareCategories)
                    .ThenInclude(hc => hc.category)
                .AsQueryable();

            if (query.hardwarestatusid >= 0)
            {
                hardwares = hardwares.Where(c => c.hardwarestatusid == query.hardwarestatusid);
            }

            if (query.typeid >= 0)
            {
                hardwares = hardwares.Where(c => c.typeid == query.typeid);
            }

            // Sorting applied BEFORE pagination
            if (query.IsDecsending)
            {
                hardwares = hardwares.OrderByDescending(h => h.id); // Ensure 'id' exists in your model
            }
            else
            {
                hardwares = hardwares.OrderBy(h => h.id);
            }

            // Pagination happens AFTER sorting
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await hardwares.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }


        public async Task<Hardware?> GetByIdAsync(int id)
        {
            return await _context.Hardware
                .Include(h => h.hardwarestatus)
                .Include(h => h.type)
                .Include(h => h.HardwareCategories)
                    .ThenInclude(hc => hc.category)
                .FirstOrDefaultAsync(h => h.id == id);
        }

        public async Task<Hardware?> UpdateAsync(int id, HardwareUpdateDTO hardwareDto, IFormFile? imageFile = null)
        {
            var existingHardware = await _context.Hardware.FirstOrDefaultAsync(x => x.id == id);

            if (existingHardware == null)
            {
                return null;
            }

            // Update the hardware properties
            existingHardware.name = hardwareDto.name;
            existingHardware.hardwarestatusid = hardwareDto.hardwarestatusid;
            existingHardware.typeid = hardwareDto.typeid;
            existingHardware.Description = hardwareDto.Description;

            // If an image file is provided, upload to Cloudinary and update the ImageUrl
            if (imageFile != null)
            {
                existingHardware.ImageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }

            await _context.SaveChangesAsync();

            return existingHardware;
        }
    }
}
