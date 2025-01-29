using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.DTOs.TypeDTOs;
using DTOs.TypeDTOs;

namespace Mappers
{
    public static class TypeMapper
    {
        public static TypeReadDTO ToTypeDto(this Types typeModel)
        {
            return new TypeReadDTO
            {
                id = typeModel.id,
                name = typeModel.name,
            };
        }
        public static Types ToTypeFromCreate(this TypeCreateDTO typeDTO)
        {
            return new Types
            {
                name = typeDTO.name,
            };
        }
    }
}