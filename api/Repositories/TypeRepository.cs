using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.TypeDTOs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class TypeRepository : ITypes
    {
        private readonly ApiDbContext _context;
        public TypeRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Types> CreateAsync(Types typeModel)
        {
            await _context.Types.AddAsync(typeModel);
            await _context.SaveChangesAsync();
            return typeModel;
        }

        public async Task<Types?> DeleteAsync(int id)
        {
            var typeModel = await _context.Types.FirstOrDefaultAsync(x => x.id == id);

            if (typeModel == null)
            {
                return null;
            }

            _context.Types.Remove(typeModel);
            await _context.SaveChangesAsync();

            return typeModel;
        }

        public async Task<List<Types>> GetAllAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<Types?> GetByIdAsync(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task<Types?> UpdateAsync(int id, TypeUpdateDTO typeDto)
        {
            var existingType = await _context.Types.FindAsync(id);

            if (existingType == null)
            {
                return null;
            }

            existingType.name = typeDto.name;

            await _context.SaveChangesAsync();

            return existingType;
        }
    }
}