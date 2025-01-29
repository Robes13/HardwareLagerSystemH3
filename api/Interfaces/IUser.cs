using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.UserDTOs;
using api.Models;
using DTOs.UserDTOs;

namespace api.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// This method will create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> CreateAsync(User user);
        /// <summary>
        /// This method will retrieve a user by id
        /// </summary>
        /// <param name="id"> the id</param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(int id);

        Task<List<User>> GetAllAsync();

        /// <summary>
        /// This method will update a user
        /// </summary>
        /// <param name="user"> The UPDATE DTO, as some data might not be updateable.</param>
        /// <returns></returns>
        Task<User?> UpdateAsync(int id, UserUpdateDTO user);

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User?> DeleteAsync(int id);


        #region Login
        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="authenticateUserDTO"></param>
        /// <returns></returns>
        Task<User?> AuthenticateAsync(AuthenticateUserDTO authenticateUserDTO);

        #endregion
    }
}