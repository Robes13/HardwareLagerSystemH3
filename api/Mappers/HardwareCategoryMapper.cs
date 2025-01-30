using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using DTOs.HardwareCategoryDTOs;

namespace api.Mappers
{
    public static class HardwareCategoryMapper
    {
        public static HardwareCategoryReadDTO ToHardwareCategoryDto(this HardwareCategory hardwarecategoryModel)
        {
            return new HardwareCategoryReadDTO
            {
                id = hardwarecategoryModel.id,
                hardware = hardwarecategoryModel.hardware?.name,
                category = hardwarecategoryModel.category?.name,
            };
        }

        public static HardwareCategory ToHardwareCategoryFromCreateDTO(this HardwareCategoryCreateDTO hardwarecategoryDto)
        {
            return new HardwareCategory
            {
                hardwareid = hardwarecategoryDto.hardwareid,
                categoryid = hardwarecategoryDto.categoryid
            };
        }

        public static HardwareCategory TohardwareCategoryFromUpdate(this HardwareCategoryUpdateDTO hardwarecategoryDto)
        {
            return new HardwareCategory
            {
                hardwareid = hardwarecategoryDto.hardwareid,
                categoryid = hardwarecategoryDto.categoryid
            };
        }
    }
}