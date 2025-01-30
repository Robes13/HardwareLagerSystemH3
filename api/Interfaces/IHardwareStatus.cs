using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareStatusDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IHardwareStatus
    {
        Task<HardwareStatus> CreateHardwareStatusAsync(HardwareStatus hardwareStatus);

        Task<HardwareStatus?> GetHardwareStatusById(int id);

        Task<List<HardwareStatus>> GetAllHardwareStatusesAsync();

        Task<HardwareStatus?> UpdateHardwareStatusAsync(int id, HardwareStatusUpdateDTO hardwareStatus);

        Task <HardwareStatus?>DeleteHardwareStatusAsync(int id);

    }
}