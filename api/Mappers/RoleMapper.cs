using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.RoleDTOs;
using api.Models;

namespace api.Mappers
{
    public static class RoleMapper
    {
        public static Role ToRoleFromCreate(this RoleCreateDTO role)
        {
            return new Role
            {
                name = role.name
            };
        }
    }
}