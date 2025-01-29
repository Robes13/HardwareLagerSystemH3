using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.RoleDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using DTOs.UserDTOs;
using Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IUser _iuser;

        public UserController(ApiDbContext context, IUser iuser)
        {
            _context = context;
            _iuser = iuser;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = userDto.ToUserFromCreate();

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return Ok($"User {user.username} created successfully");
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with id: {id} not found");
            }

            await _iuser.DeleteAsync(id);

            return Ok($"User with id: {id} deleted successfully");
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _iuser.GetAllAsync();
            return Ok(user);
        }
    }
}