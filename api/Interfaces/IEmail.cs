using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.DTOs.EmailDTOs;

namespace api.Interfaces
{
    public interface IEmail
    {
        /// <summary>
        /// Gets an email by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Email?> GetByIdAsync(int id);

        /// <summary>
        /// Creates an email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Email> CreateEmailAsync(Email email);

        /// <summary>
        /// Updates an email, finds the email to update through the id, and updates the email accordingly using the emailUpdateDTO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailUpdateDTO"></param>
        /// <returns></returns>
        Task<Email?> UpdateAsync(int id, EmailUpdateDTO emailUpdateDTO);

        /// <summary>
        /// Deletes an email. It will only be used by the user controller to also delete their email in the system.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Email?> DeleteAsync(int id);

        /// <summary>
        /// Finds the email through the id, and verifies it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool?> CheckEmailValidityAsync(int id);

        /// <summary>
        /// Finds the email through the id, and returns it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Email?> GetSecretKeyAsync(int id);

        Task<Email?>UpdateVerify(CheckEmailSecretCodeDTO updateVerifyDTO);
    }
}