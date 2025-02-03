using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.EmailDTOs
{
    public class CheckEmailSecretCodeDTO
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}