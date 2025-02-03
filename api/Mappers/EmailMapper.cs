using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.EmailDTOs;
using api.Models;

namespace api.Mappers
{
    public static class EmailMapper
    {
        public static EmailDTO ToEmailDTO(this Email email)        
        {
            return new EmailDTO
            {
                EmailAddress = email.EmailAddress,
                IsVerified = email.IsVerified,
            };
        }

        public static Email ToEmailFromCreateDTO(this CreateEmailDTO createEmailDTO)
        {
            return new Email
            {
                EmailAddress = createEmailDTO.EmailAddress,
                IsVerified = false,
                SecretKey = "",
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}