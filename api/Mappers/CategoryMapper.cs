using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.DTOs.CategoryDTOs;
using DTOs.CategoryDTOs;

namespace Mappers
{
    public static class CategoryMapper
    {
        public static CategoryReadDTO ToCategoryDto(this Category categoryModel)
        {
            return new CategoryReadDTO
            {
                id = categoryModel.id,
                name = categoryModel.name,
            };
        }
        public static Category ToCategoryFromCreate(this CategoryCreateDTO categoryDTO)
        {
            return new Category
            {
                name = categoryDTO.name,
            };
        }
    }
}