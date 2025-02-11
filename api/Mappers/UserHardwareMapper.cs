using System;
using api.DTOs.UserHardwareDTOs;
using api.Models;

namespace api.Mappers
{
    public static class UserHardwareMapper
    {
        public static UserHardware MapToUserHardware(this CreateUserHardwareDTO dto)
        {
            return new UserHardware
            {
                userid = dto.UserId,
                hardwareid = dto.HardwareId,
                startDate = dto.GetStartDateTime(),
                endDate = dto.GetEndDateTime(),
                deliveryDate = null,
                isRented = true
            };
        }

        public static ReadUserHardwareDTO MapToReadUserHardwareDTO(this UserHardware userHardware)
        {
            return new ReadUserHardwareDTO
            {
                id = userHardware.id,
                fullName = userHardware.User?.username ?? "Unknown User",
                hardware = userHardware.Hardware?.name ?? "Unknown Hardware",
                startDate = userHardware.startDate.ToString("yyyy-MM-dd-HH:mm"),
                endDate = userHardware.endDate.ToString("yyyy-MM-dd-HH:mm"),
                deliveryDate = userHardware.deliveryDate,
                isRented = userHardware.isRented
            };
        }
    }
}
