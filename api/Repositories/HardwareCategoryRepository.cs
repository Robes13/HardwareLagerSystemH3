using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using DTOs.HardwareCategoryDTOs;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class HardwareCategoryRepository : IHardwareCategory
    {
        private readonly ApiDbContext _context;
        public HardwareCategoryRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<HardwareCategory>> GetAllAsync(HardwareCategoryQueryObject query)
        {
            var hardwarecategories = _context.HardwareCategory
                .Include(h => h.hardware)
                .Include(h => h.category)
                .AsQueryable();

            if (query.hardwareid >= 0)
            {
                hardwarecategories = hardwarecategories.Where(c => c.hardwareid == query.hardwareid);
            }

            if (query.categoryid >= 0)
            {
                hardwarecategories = hardwarecategories.Where(c => c.categoryid == query.categoryid);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await hardwarecategories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<HardwareCategory?> GetByIdAsync(int id)
        {
            return await _context.HardwareCategory
                .Include(h => h.hardware)
                .Include(h => h.category)
                .FirstOrDefaultAsync(h => h.id == id);
        }

        public async Task<HardwareCategory> CreateAsync(HardwareCategory hardwarecategoryModel)
        {
            await _context.HardwareCategory.AddAsync(hardwarecategoryModel);
            await _context.SaveChangesAsync();
            return hardwarecategoryModel;
        }

        public async Task<HardwareCategory?> UpdateAsync(int id, HardwareCategoryUpdateDTO hardwarecategoryDto)
        {
            var existingHardwareCategory = await _context.HardwareCategory.FirstOrDefaultAsync(x => x.id == id);

            if (existingHardwareCategory == null)
            {
                return null;
            }

            existingHardwareCategory.hardwareid = hardwarecategoryDto.hardwareid;
            existingHardwareCategory.categoryid = hardwarecategoryDto.categoryid;

            await _context.SaveChangesAsync();

            return existingHardwareCategory;
        }

        public async Task<HardwareCategory?> DeleteAsync(int id)
        {
            var hardwarecategoryModel = await _context.HardwareCategory.FirstOrDefaultAsync(x => x.id == id);

            if (hardwarecategoryModel == null)
            {
                return null;
            }

            _context.HardwareCategory.Remove(hardwarecategoryModel);
            await _context.SaveChangesAsync();
            return hardwarecategoryModel;
        }

        public async Task<bool> HardwareCategoryExists(int id)
        {
            return await _context.HardwareCategory.AnyAsync(s => s.id == id);
        }
    }
}