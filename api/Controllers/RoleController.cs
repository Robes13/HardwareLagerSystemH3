using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.RoleDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IRole _role;
        public RoleController(IRole role, ApiDbContext context)
        {
            _context = context;
            _role = role;
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleCreateDTO roleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleModel = roleDTO.ToRoleFromCreate();

            // Call repository to save the role
            var createdRole = await _role.CreateAsync(roleModel);

            return Ok("Role created successfully");
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await _role.GetAllAsync();
            return Ok(roles);
        }
        
        [HttpGet]
        [Route("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _role.GetAsync(id);
            if (role == null)
            {
                return NotFound($"Role with id {id} not found");
            }
            return Ok(role);
        }
        
        [HttpPut]
        [Route("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleUpdateDTO roleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var role = await _role.GetAsync(id);

            if (role == null)
            {
                return NotFound($"Role with id {id} not found");
            }
            
            role.name = roleDTO.name;
            
            await _context.SaveChangesAsync();
            
            return Ok("Role updated successfully");
        }
        
        [HttpDelete]
        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var role = await _role.DeleteAsync(id);
            
            if (role == null)
            {
                return NotFound($"Role with id {id} not found");
            }
            
            await _context.SaveChangesAsync();
            
            return Ok($"Role: {role.name} was deleted successfully");
        }
    }
}
