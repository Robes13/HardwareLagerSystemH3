using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.RoleDTOs;
using api.DTOs.UserDTOs;
using api.Interfaces;
using api.Models;
using DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Engines;

namespace api.Repositories
{
    public class UserRepository : IUser
    {
        private readonly ApiDbContext _context;
        public UserRepository(ApiDbContext context)
        {
            _context = context;
        }
        public Task<User?> AuthenticateAsync(AuthenticateUserDTO authenticateUserDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// TO DO: ENCRYPT DELETED USERS NAME.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> DeleteAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            user.isdeleted = true;
            user.fullname = "deleted";
            user.datedeleted = DateTime.UtcNow; // Use UTC time for consistency

            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error if needed
                throw new Exception("Error deleting user", ex);
            }

            return user;
        }

        public async Task<List<UserReadDTO>> GetAllAsync()
        {
            var users = await _context.User
                .OrderBy(x => x.id)
                .Include(x => x.Role)  // Include Role entity
                .ToListAsync();

            // Map the users to UserDTO
            var userDTOs = users.Select(user => new UserReadDTO
            {
                Id = user.id,
                Username = user.username,
                HashedPassword = user.hashedpassword,
                Email = user.email,
                Fullname = user.fullname,
                IsDeleted = user.isdeleted,
                IsVerified = user.isVerified,
                RoleId = user.roleid,
                Role = new RoleReadDTO  // Map Role to RoleDTO
                {
                    RoleName = user.Role.name
                }
            }).ToList();

            return userDTOs;
        }

        public async Task<UserReadDTO?> GetByIdAsync(int id)
        {
            var user = await _context.User.Include(x => x.Role).FirstOrDefaultAsync(x => x.id == id);
            if (user == null)
            {
                return null;
            }

            // Map to UserDTO
            return new UserReadDTO
            {
                Id = user.id,
                Username = user.username,
                HashedPassword = user.hashedpassword,
                Email = user.email,
                Fullname = user.fullname,
                IsVerified = user.isVerified,
                IsDeleted = user.isdeleted,
                RoleId = user.roleid,
                Role = new RoleReadDTO
                {
                    RoleName = user.Role.name
                }
            };
        }


        public async Task<User?> UpdateAsync(int id, UserUpdateDTO user)
        {
            var userModel = await _context.User.FindAsync(id);
            if (userModel == null)
            {
                return null;
            }

            var roleExists = await _context.Role.AnyAsync(asasa => asasa.id == user.roleid);
            if (!roleExists)
            {
                return null;
            }

            userModel.username = user.username;
            userModel.fullname = user.fullname;
            userModel.email = user.email;
            userModel.hashedpassword = user.hashedpassword;
            userModel.isVerified = user.isVerified;
            userModel.roleid = user.roleid;  

            await _context.SaveChangesAsync();

            return userModel;
        }


    }
}
