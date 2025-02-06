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
        // Read
        public static ReadUserHardwareDTO Read(this UserHardware userHardware)
        {
            return new ReadUserHardwareDTO
            {
                id = userHardware.id,
                fullName = userHardware.User?.username,
                hardware = userHardware.Hardware?.name,
                startDate = userHardware.startDate,
                endDate = userHardware.endDate,
                deliveryDate = userHardware.deliveryDate,
                isRented = userHardware.isRented
            };
        }
    }
}

