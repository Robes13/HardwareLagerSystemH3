using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.UserHardwareDTOs;
using api.Helpers.QueryObjects;
using api.Models;
using DTOs.HardwareDTOs;

namespace api.Interfaces
{
    public interface IUserHardware
    {
        Task<List<HardwareReadDTO>> GetAvailableHardware(
               List<int>? categoryIds,
               List<int>? typeIds,
               string? searchString,
               DateTime startDate,
               DateTime endDate);         
               Task<List<UserHardware>> GetUserHardwareByUserId(int userId);
        Task<ReadUserHardwareDTO?> AddUserHardware(CreateUserHardwareDTO userHardware);
        Task<UserHardware?> UpdateUserHardware(int id, UpdateUserHardwareDTO userHardware);
        Task<bool> Exists(int UserHardwareId);
    }
}