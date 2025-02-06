using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.DTOs.CategoryDTOs;
using api.Helpers.QueryObjects;

namespace api.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly ApiDbContext _context;
        public CategoryRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.Category.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _context.Category.FirstOrDefaultAsync(x => x.id == id);

            if (categoryModel == null)
            {
                return null;
            }

            _context.Category.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Category.AnyAsync(s => s.id == id);
        }

        public async Task<List<Category>> GetAllAsync(CategoryQueryObject query)
        {
            var categories = _context.Category.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.name))
            {
                categories = categories.Where(c => c.name.Contains(query.name));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await categories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Category.FindAsync(id);
        }
        public async Task<Category?> UpdateAsync(int id, CategoryUpdateDTO categoryDto)
        {
            var existingCategory = await _context.Category.FirstOrDefaultAsync(x => x.id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.name = categoryDto.name;

            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}