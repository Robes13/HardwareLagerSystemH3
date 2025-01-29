using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using DTOs.UserDTOs;

namespace Mappers
{
    public static class UserMapper
    {
        public static User ToUserFromCreate(this UserCreateDTO user)
        {
            return new User
            {
                username = user.username,
                hashedpassword = user.hashedpassword,
                email = user.email,
                fullname = user.fullname,
                roleid = 1,
            };
        }
        public static User ToUserFromUpdate(this UserUpdateDTO user)
        {
            
            return new User
            {
                username = user.username,
                hashedpassword = user.hashedpassword,
                email = user.email,
                fullname = user.fullname,
                isVerified = user.isVerified,
                roleid = user.roleid,
            };
        }
    }
}