using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.EmailDTOs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EmailRepository : IEmail
    {
        private readonly ApiDbContext _context;
        public EmailRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<bool?> CheckEmailValidityAsync(int id)
        {

            var email = await _context.Email.FirstOrDefaultAsync(e => e.Id == id);

            if (email == null)
            {
                return null;
            }
            if (email.IsVerified)
            {
                return true;
            }
            return false;
        }

        public async Task<Email> CreateEmailAsync(Email email)
        {
            // Check if the email already exists in the database
            var existingEmail = await _context.Email
                .FirstOrDefaultAsync(e => e.EmailAddress == email.EmailAddress);

            if (existingEmail != null)
            {
                // If the email exists, you can either throw an exception or handle the response accordingly
                throw new InvalidOperationException("This email address is already in use.");
            }

            // If the email doesn't exist, proceed to add it to the database
            await _context.Email.AddAsync(email);
            await _context.SaveChangesAsync();
            return email;
        }


        public async Task<Email?> DeleteAsync(int id)
        {
            var email = await _context.Email.FirstOrDefaultAsync(e => e.Id == id);
            if (email == null)
            {
                return null;
            }
            _context.Email.Remove(email);
            await _context.SaveChangesAsync();
            return email;
        }

        public async Task<Email?> GetByIdAsync(int id)
        {
            return await _context.Email.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Email?> GetSecretKeyAsync(int id)
        {
            return await _context.Email.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Email?> UpdateAsync(int id, EmailUpdateDTO emailUpdateDTO)
        {
            var existingEmail = await _context.Email.FirstOrDefaultAsync(e => e.Id == id);
            if (existingEmail == null)
            {
                return null;
            }
            existingEmail.EmailAddress = emailUpdateDTO.EmailAddress;
            await _context.SaveChangesAsync();
            return existingEmail;
        }

        public async Task<Email?> UpdateVerify(CheckEmailSecretCodeDTO updateVerifyDTO)
        {
            // Retrieve the existing email from the database by email address
            var existingEmail = await _context.Email.FirstOrDefaultAsync(e => e.EmailAddress == updateVerifyDTO.EmailAddress);

            if (existingEmail == null)
            {
                // If no email is found, return null
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(updateVerifyDTO.SecretKey, existingEmail.SecretKey))
            {
                // If the SecretKey is valid, update the verification status
                existingEmail.IsVerified = true;
                await _context.SaveChangesAsync();  // Save the changes to the database
                return existingEmail;  // Return the updated email object
            }
            else
            {
                // If the SecretKey does not match, return null
                return null;
            }
        }

        public async Task<Email?> GetByEmailAsync(string emailaddress)
        {
            return await _context.Email.FirstOrDefaultAsync(u => u.EmailAddress == emailaddress);
        }
    }
}