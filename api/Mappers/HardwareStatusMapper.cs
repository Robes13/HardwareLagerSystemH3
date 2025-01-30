using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareStatusDTOs;
using api.Models;

namespace api.Mappers
{
    public static class HardwareStatusMapper
    {
        public static HardwareStatus ToHardwareStatusFromCreate(this HardwareStatusUpdateDTO status)
        {
            return new HardwareStatus
            {
                name = status.name
            };
        }   
    }
}