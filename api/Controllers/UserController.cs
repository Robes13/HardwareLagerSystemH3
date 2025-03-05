using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.RoleDTOs;
using api.DTOs.UserDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Services;
using DTOs.UserDTOs;
using Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IUser _iuser;
        private readonly JwtTokenService _jwtTokenService;

        public UserController(ApiDbContext context, IUser iuser, JwtTokenService jwtTokenService)
        {
            _context = context;
            _iuser = iuser;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userDto.ToUserFromCreate();

            // Find the Role with the given roleid
            var role = await _context.Role.FindAsync(user.roleid);
            if (role == null)
            {
                return NotFound($"Role with id {user.roleid} not found");
            }
            var email = await _context.Email.FindAsync(user.EmailId);
            if (email == null)
            {
                return NotFound($"Email with id {user.EmailId} not found");
            }
            // Add the user to the role's users collection
            role.users.Append(user);
            user.Role = role;
            // Add the user to the User table and save the changes
            await _iuser.CreateAsync(user);
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

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _iuser.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with id: {id} not found");
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO userDto)
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

            // Validate if the roleid exists in the Role table before updating
            var roleExists = await _context.Role.AnyAsync(r => r.id == userDto.roleid);
            if (!roleExists)
            {
                return BadRequest($"Role with id {userDto.roleid} does not exist.");
            }

            user.username = userDto.username;
            user.hashedpassword = userDto.hashedpassword;
            user.roleid = userDto.roleid;

            await _context.SaveChangesAsync();
            return Ok($"User with id: {id} updated successfully");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDTO authenticateDTO)
        {
            if (authenticateDTO == null || string.IsNullOrEmpty(authenticateDTO.Username) || string.IsNullOrEmpty(authenticateDTO.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            var user = await _iuser.GetByUsernameAsync(authenticateDTO.Username);  // Ensure this method handles role loading

            if (user == null || user.Role == null || !BCrypt.Net.BCrypt.Verify(authenticateDTO.Password, user.hashedpassword))
            {
                return Unauthorized("Invalid Username, Password, or Role is not assigned.");
            }

            string token = _jwtTokenService.GenerateToken(user.id.ToString(), user.Role.id.ToString());
            return Ok(new
            {
                message = "Login successful!",
                token = token,
                user.fullname,
                user.id
            });
        }
    }
}