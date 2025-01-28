using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IHardwareCategory
    {
        Task<List<HardwareCategory>> GetAllAsync(QueryObject query);
        Task<HardwareCategory?> GetByIdAsync(int id);
        Task<HardwareCategory> CreateAsync(HardwareCategory hardwarecategoryModel);
        Task<HardwareCategory?> UpdateAsync(int id, UpdateHardwareCategoryRequestDto hardwarecategoryDto);
        Task<HardwareCategory?> DeleteAsync(int id);
        Task<bool> HardwareCategoryExists(int id);
    }
}