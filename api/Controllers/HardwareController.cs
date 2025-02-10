using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareDTOs;
using api.Helpers.QueryObjects;
using api.Interfaces;
using api.Repositories;
using DTOs.HardwareDTOs;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace api.Controllers
{
    [Route("api/hardware")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly IHardware _hardwareRepo;
        private readonly ITypes _typeRepo;
        private readonly IHardwareStatus _hardwarestatusRepo;
        private readonly CloudinaryService _cloudinaryService; // Inject CloudinaryService

        public HardwareController(IHardware hardwareRepo, ITypes typeRepo, IHardwareStatus hardwarestatusRepo, CloudinaryService cloudinaryService)
        {
            _hardwareRepo = hardwareRepo;
            _typeRepo = typeRepo;
            _hardwarestatusRepo = hardwarestatusRepo;
            _cloudinaryService = cloudinaryService; // Initialize CloudinaryService
        }

        [HttpGet]
        [Route("GetAllHardwares")]
        public async Task<IActionResult> GetAll([FromQuery] HardwareQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwares = await _hardwareRepo.GetAllAsync(query);

            var hardwareDto = hardwares.Select(s => s.ToHardwareDto()).ToList();

            return Ok(hardwareDto);
        }

        [HttpGet]
        [Route("GetByIdHardware/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardware = await _hardwareRepo.GetByIdAsync(id);

            if (hardware == null)
            {
                return NotFound("Hardware not found.");
            }

            return Ok(hardware.ToHardwareDto());
        }

        [HttpPost]
        [Route("CreateHardware")]
        public async Task<IActionResult> Create([FromForm] HardwareCreateDTO hardwareDto, [FromForm] IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool hardwarestatusExists = await _hardwarestatusRepo.ExistsAsync(Convert.ToInt32(hardwareDto.hardwarestatusid));
            if (!hardwarestatusExists)
            {
                return NotFound("Invalid HardwareStatus ID.");
            }

            bool typeExists = await _typeRepo.ExistsAsync(Convert.ToInt32(hardwareDto.typeid));
            if (!typeExists)
            {
                return NotFound("Invalid Type ID.");
            }

            // Handle image upload if file is provided
            string imageUrl = string.Empty;
            if (imageFile != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }

            var hardwareModel = hardwareDto.ToHardwareFromCreate();
            hardwareModel.ImageUrl = imageUrl;  // Save the image URL

            await _hardwareRepo.CreateAsync(hardwareModel);

            return Ok("Hardware Added!");
        }

        [HttpPut]
        [Route("UpdateHardware/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] HardwareUpdateDTO updateDto, [FromForm] IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool hardwarestatusExists = await _hardwarestatusRepo.ExistsAsync(updateDto.hardwarestatusid);
            if (!hardwarestatusExists)
            {
                return NotFound("Invalid HardwareStatus ID.");
            }

            bool typeExists = await _typeRepo.ExistsAsync(updateDto.typeid);
            if (!typeExists)
            {
                return NotFound("Invalid Type ID.");
            }

            // Handle image upload if file is provided
            string imageUrl = string.Empty;
            if (imageFile != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
            }

            var hardwareModel = await _hardwareRepo.UpdateAsync(id, updateDto);

            if (hardwareModel == null)
            {
                return NotFound("ID does not match any Hardware");
            }

            // If an image URL was generated, update the hardware model
            if (!string.IsNullOrEmpty(imageUrl))
            {
                hardwareModel.ImageUrl = imageUrl;
                await _hardwareRepo.UpdateAsync(id, updateDto); // Save the updated hardware model with the new image URL
            }

            return Ok("The Hardware was updated!");
        }

        [HttpDelete]
        [Route("DeleteHardware/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwareModel = await _hardwareRepo.DeleteAsync(id);

            if (hardwareModel == null)
            {
                return NotFound("No Hardware found to delete.");
            }

            return NoContent();
        }
    }
}
