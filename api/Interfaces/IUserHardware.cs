using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.UserHardwareDTOs;
using api.Helpers.QueryObjects;
using api.Models;

namespace api.Interfaces
{
    public interface IUserHardware
    {
        Task<List<Hardware>> GetAvailableHardware(List<int> categoryIds, List<int> typeIds, int weeks, string searchString);
        Task<List<UserHardware>> GetUserHardwareByUserId(int userId);
        Task<UserHardware?> AddUserHardware(UserHardware userHardware);
        Task<UserHardware?> UpdateUserHardware(int id, UpdateUserHardwareDTO userHardware);
        Task<bool> Exists(int UserHardwareId);
    }
}