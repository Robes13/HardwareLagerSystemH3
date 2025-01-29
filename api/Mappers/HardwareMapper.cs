using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using DTOs.HardwareDTOs;

namespace Mappers
{
    public static class HardwareMapper
    {
        public static HardwareReadDTO ToHardwareDto(this Hardware hardwareModel)
        {
            return new HardwareReadDTO
            {
                id = hardwareModel.id,
                name = hardwareModel.name,
                hardwarestatus = hardwareModel.hardwarestatus?.name,
                type = hardwareModel.type?.name,
                hardwarecategories = hardwareModel.HardwareCategories?.Select(c => c.name).ToList() ?? new List<string>()
            };
        }

        public static Hardware ToHardwareFromCreate(this HardwareCreateDTO hardwareDto)
        {
            return new Hardware
            {
                name = hardwareDto.name,
                hardwarestatusid = hardwareDto.hardwarestatusid,
                typeid = hardwareDto.typeid
            };
        }
    }
}