using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.RoleDTOs;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly ApiDbContext _context;
        public RoleRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Role.OrderBy(x => x.id).ToListAsync();
        }
        public async Task<Role?> DeleteAsync(int id)
        {
            var role = await _context.Role.FindAsync(id);

            if (role == null)
            {
                return null;
            }
            _context.Role.Remove(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role?> GetAsync(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<Role?> UpdateAsync(int id, RoleUpdateDTO roleUpdateDTO)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return null;
            }
            role.name = roleUpdateDTO.name;
            await _context.SaveChangesAsync();

            return role;
        }
    }
}