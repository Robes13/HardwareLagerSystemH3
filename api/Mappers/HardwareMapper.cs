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
                Description = hardwareModel.Description,
                hardwarestatus = hardwareModel.hardwarestatus?.name,
                type = hardwareModel.type?.name,
                hardwarecategories = hardwareModel.HardwareCategories?
                    .Select(c => c.category.name)
                    .Where(name => name != null)
                    .ToList() ?? new List<string>(),
                ImageUrl = hardwareModel.ImageUrl
            };
        }

        public static Hardware ToHardwareFromCreate(this HardwareCreateDTO hardwareDto)
        {
            return new Hardware
            {
                name = hardwareDto.name,
                hardwarestatusid = Convert.ToInt32(hardwareDto.hardwarestatusid),
                typeid = Convert.ToInt32(hardwareDto.typeid),
                Description = hardwareDto.Description
            };
        }
    }
}