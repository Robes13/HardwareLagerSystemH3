using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.UserHardwareDTOs;
using api.Models;

namespace api.Mappers
{
    public static class UserHardwareMapper
    {
        public static UserHardware Rent(this CreateUserHardwareDTO dto)
        {
            return new UserHardware
            {
                userid = dto.UserId,
                hardwareid = dto.HardwareId,
                startDate = dto.StartDate,
                endDate = dto.EndDate,
                deliveryDate = dto.DeliveryDate,
                isRented = true 
            };
        }
    }
}

