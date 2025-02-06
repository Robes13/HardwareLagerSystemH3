using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.UserHardwareDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/userhardware")]
    public class UserHardwareController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IUserHardware _userHardware;

        public UserHardwareController(ApiDbContext context, IUserHardware userHardware)
        {
            _context = context;
            _userHardware = userHardware;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableHardware(
       [FromQuery] List<int> categoryIds,
       [FromQuery] List<int> typeIds,
       [FromQuery] int weeks,
       [FromQuery] string searchString = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var availableHardware = await _userHardware.GetAvailableHardware(categoryIds, typeIds, weeks, searchString);
            return Ok(availableHardware);
        }

        //Get hardwares by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserHardware>> GetByUserId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userHardware = await _userHardware.GetUserHardwareByUserId(id);
            if (userHardware == null)
            {
                return NotFound("Couldn't find any hardware for user");
            }
            return Ok(userHardware);
        }

        //Add a new hardware to a user
        [HttpPost]
        public async Task<ActionResult<ReadUserHardwareDTO>> AddUserHardware(CreateUserHardwareDTO userHardwareDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userHardware.AddUserHardware(userHardwareDto);
            await _context.SaveChangesAsync();

            return Ok(userHardwareDto.Rent().Read());
        }


        //Update a user hardware
        [HttpPut("{id:int}")]
        public async Task<IActionResult?> UpdateUserHardware([FromRoute] int id, UpdateUserHardwareDTO userHardware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserHardware? hardware = await _userHardware.UpdateUserHardware(id, userHardware);
            if (hardware == null)
            {
                return NotFound("Couldn't find user hardware with given id");
            }
            await _context.SaveChangesAsync();
            return Ok("User hardware updated.");
        }

        //Check if a user hardware exists
        [HttpGet("exists/{id:int}")]
        public async Task<IActionResult> UserHardwareExists([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool exists = await _userHardware.Exists(id);
            return Ok(exists);
        }
    }
}