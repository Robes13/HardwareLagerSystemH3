using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.EmailDTOs
{
    public class UpdateVerifyDTO
    {
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string SecretKey { get; set; } = string.Empty;
    }
}