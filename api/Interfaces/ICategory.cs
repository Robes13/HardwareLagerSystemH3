using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.CategoryDTOs;
using api.Helpers.QueryObject;
using api.Models;

namespace api.Interfaces
{
    public interface ICategory
    {
        Task<List<Category>> GetAllAsync(CategoryQueryObject query);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, CategoryUpdateDTO categoryDto);
        Task<Category?> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

    }
}