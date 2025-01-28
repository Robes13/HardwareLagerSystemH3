using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class HardwareCategoryRepository : IHardwareCategory
    {
        private readonly ApiDbContext _context;
        public HardwareCategoryRepository(ApiDbContext context)
        {
            _context = context;
        }

        public Task<List<HardwareCategory>> GetAllAsync(QueryObject query)
        {
            
        }

        public Task<HardwareCategory?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HardwareCategory> CreateAsync(HardwareCategory hardwarecategoryModel)
        {
            throw new NotImplementedException();
        }

        public Task<HardwareCategory?> UpdateAsync(int id, UpdateHardwareCategoryRequestDto hardwarecategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<HardwareCategory?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HardwareCategoryExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}