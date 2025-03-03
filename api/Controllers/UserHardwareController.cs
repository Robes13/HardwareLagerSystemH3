using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.UserHardwareDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.DTOs.HardwareDTOs;
using DTOs.HardwareDTOs;

namespace api.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        [HttpGet("GetMostLoaned")]
        public async Task<ActionResult<List<HardwareReadDTO>>> GetAll()
        {
            try
            {
                // Get the ordered hardware list by frequency from the async method
                var orderedHardwares = await _userHardware.GetAllAsync();

                // Map the result to DTOs (Data Transfer Objects)
                var hardwareDTOs = orderedHardwares.Select(hardwareModel => new HardwareReadDTO
                {
                    id = hardwareModel.id,
                    name = hardwareModel.name,
                    Description = hardwareModel.Description,
                    hardwarestatus = hardwareModel.hardwarestatus?.name,
                    type = hardwareModel.type?.name,
                    hardwarecategories = hardwareModel.HardwareCategories?
                        .Select(c => c.category.name)
                        .Where(name => name != null)
                        .ToList() ?? new List<string>(),
                    ImageUrl = hardwareModel.ImageUrl
                }).ToList();

                // Return the list of hardware as DTOs
                return Ok(hardwareDTOs);
            }
            catch (Exception ex)
            {
                // Handle any exceptions gracefully
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableHardware([FromQuery] List<int>? categoryIds, [FromQuery] List<int>? typeIds, [FromQuery] int weeks, [FromQuery] string? searchString, [FromQuery] DateTime startDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Calculate the end date based on the provided startDate and weeks
            DateTime finalEndDate = startDate.AddDays(weeks * 7);

            var availableHardware = await _userHardware.GetAvailableHardware(
                categoryIds,
                typeIds,
                searchString,
                startDate,
                finalEndDate
            );

            return Ok(availableHardware);
        }

        [HttpGet("GetUserLoanHistory/{id:int}")]
        public async Task<ActionResult<List<HardwareReadDTO>>> GetLoanHistoryForUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userHardwareRecords = await _userHardware.GetUserLoanHistoryAsync(id);

            if (userHardwareRecords == null || !userHardwareRecords.Any())
            {
                return Ok(new List<HardwareReadDTO>());
            }

            var hardwareDTOs = userHardwareRecords.Select(uh => new HardwareReadDTO
            {
                id = uh.Hardware.id,
                name = uh.Hardware.name,
                Description = uh.Hardware.Description,
                hardwarestatus = uh.Hardware.hardwarestatus?.name,
                type = uh.Hardware.type?.name,
                hardwarecategories = uh.Hardware.HardwareCategories?
                    .Select(c => c.category.name)
                    .Where(name => name != null)
                    .ToList() ?? new List<string>(),
                ImageUrl = uh.Hardware.ImageUrl
            }).ToList();

            // Reverse the list before returning it
            hardwareDTOs.Reverse();

            return Ok(hardwareDTOs);
        }
        [HttpGet("GetActiveLoansByUserId/{id:int}")]
        public async Task<ActionResult<List<HardwareReadDTO>>> GetActiveLoansByUserId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var activeLoans = await _userHardware.GetActiveLoansByUserAsync(id);
            if (activeLoans == null || !activeLoans.Any())
            {
                return Ok(new List<HardwareReadDTO>());
            }
            var hardwareDTOs = activeLoans.Select(uh => new HardwareReadDTO
            {
                id = uh.Hardware.id,
                name = uh.Hardware.name,
                Description = uh.Hardware.Description,
                hardwarestatus = uh.Hardware.hardwarestatus?.name,
                type = uh.Hardware.type?.name,
                hardwarecategories = uh.Hardware.HardwareCategories?
                    .Select(c => c.category.name)
                    .Where(name => name != null)
                    .ToList() ?? new List<string>(),
                ImageUrl = uh.Hardware.ImageUrl
            }).ToList();
            return Ok(hardwareDTOs);
        }
        // Add a new hardware to a user
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ReadUserHardwareDTO>> AddUserHardware(CreateUserHardwareDTO userHardwareDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Use the mapping extension in the repository (which calls MapToUserHardware)
            var result = await _userHardware.AddUserHardware(userHardwareDto);
            if (result == null)
            {
                return NotFound("User or hardware not found.");
            }
            // Save is already called in the repository, but if you need an extra save, you can leave this here.
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        // Update a user hardware
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
        [AllowAnonymous]
        //Check if a user hardware exists
        // Check if a user hardware exists
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
