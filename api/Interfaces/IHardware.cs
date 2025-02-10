using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareDTOs;
using api.Helpers.QueryObjects;
using api.Models;

namespace api.Interfaces
{
    public interface IHardware
    {
        Task<List<Hardware>> GetAllAsync(HardwareQueryObject query);
        Task<Hardware?> GetByIdAsync(int id);
        Task<Hardware> CreateAsync(Hardware hardwareModel, IFormFile? imageFile = null);
        Task<Hardware?> UpdateAsync(int id, HardwareUpdateDTO hardwareDto, IFormFile? imageFile = null);
        Task<Hardware?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}