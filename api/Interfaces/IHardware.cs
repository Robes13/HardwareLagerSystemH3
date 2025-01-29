using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareDTOs;
using api.Helpers.QueryObject;
using api.Models;

namespace api.Interfaces
{
    public interface IHardware
    {
        Task<List<Hardware>> GetAllAsync(HardwareQueryObject query);
        Task<Hardware?> GetByIdAsync(int id);
        Task<Hardware> CreateAsync(Hardware hardwareModel);
        Task<Hardware?> UpdateAsync(int id, HardwareUpdateDTO hardwareDto);
        Task<Hardware?> DeleteAsync(int id);
    }
}