using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;
using DTOs.HardwareCategoryDTOs;

namespace api.Interfaces
{
    public interface IHardwareCategory
    {
        Task<List<HardwareCategory>> GetAllAsync(HardwareCategoryQueryObject query);
        Task<HardwareCategory?> GetByIdAsync(int id);
        Task<HardwareCategory> CreateAsync(HardwareCategory hardwarecategoryModel);
        Task<HardwareCategory?> UpdateAsync(int id, HardwareCategoryUpdateDTO hardwarecategoryDto);
        Task<HardwareCategory?> DeleteAsync(int id);
        Task<bool> HardwareCategoryExists(int id);
    }
}