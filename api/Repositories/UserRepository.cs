using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.UserDTOs;
using api.Interfaces;
using api.Models;
using DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;

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


        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.OrderBy(x => x.id).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> UpdateAsync(int id, UserUpdateDTO user)
        {
            var userModel = await _context.User.FindAsync(id);
            if (userModel == null)
            {
                return null;
            }
            userModel.fullname = user.fullname;
            userModel.email = user.email;
            userModel.hashedpassword = user.hashedpassword;
            userModel.isVerified = user.isVerified;
            userModel.roleid = user.roleid;
            userModel.Role = user.Role;
            userModel.username = user.username;

            await _context.SaveChangesAsync();
            return userModel;
        }
    }
}