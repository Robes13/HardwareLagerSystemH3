using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareDTOs;
using api.Helpers.QueryObject;
using api.Interfaces;
using api.Repositories;
using DTOs.HardwareDTOs;
using Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/hardware")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly IHardware _hardwareRepo;
        private readonly ITypes _typeRepo;
        public HardwareController(IHardware hardwareRepo, ITypes typeRepo)
        {
            _hardwareRepo = hardwareRepo;
            _typeRepo = typeRepo;
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
        public async Task<IActionResult> Create([FromBody] HardwareCreateDTO hardwareDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool typeExists = await _typeRepo.ExistsAsync(hardwareDto.typeid);
            if (!typeExists)
            {
                return NotFound("Invalid Type ID.");
            }

            var hardwareModel = hardwareDto.ToHardwareFromCreate();
            await _hardwareRepo.CreateAsync(hardwareModel);

            return Ok("Hardware Added!");
        }

        [HttpPut]
        [Route("UpdateHardware/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] HardwareUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwareModel = await _hardwareRepo.UpdateAsync(id, updateDto);

            if (hardwareModel == null)
            {
                return NotFound("ID does not match any Hardware");
            }

            return Ok(hardwareModel.ToHardwareDto());
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