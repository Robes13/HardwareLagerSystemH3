using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.RoleDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IRole
    {
        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<Role> CreateAsync(Role role);

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns></returns>
        Task<List<Role>> GetAllAsync();
        /// <summary>
        /// Updates a role, it first finds the id if it exists, and then updates it with the updateDTO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleUpdateDTO"></param>
        /// <returns></returns>
        Task<Role?> UpdateAsync(int id, RoleUpdateDTO roleUpdateDTO);

        /// <summary>
        /// Deletes a role if it exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Role?> DeleteAsync(int id);

        /// <summary>
        /// Gets a role if exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Role?> GetAsync(int id);
    }
}