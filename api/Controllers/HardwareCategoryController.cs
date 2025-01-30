using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using DTOs.HardwareCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/hardwarecategory")]
    [ApiController]
    public class HardwareCategoryController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IHardwareCategory _hardwarecategoryRepo;
        private readonly ICategory _categoryRepo;
        private readonly IHardware _hardwareRepo;

        public HardwareCategoryController(ApiDbContext context, IHardwareCategory hardwarecategoryRepo, ICategory categoryRepo, IHardware hardwareRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _hardwarecategoryRepo = hardwarecategoryRepo;
            _hardwareRepo = hardwareRepo;
        }

        [HttpGet]
        [Route("GetAllHardwareCategories")]
        public async Task<IActionResult> GetAll([FromQuery] HardwareCategoryQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwarecategory = await _hardwarecategoryRepo.GetAllAsync(query);

            var hardwarecategoryDto = hardwarecategory.Select(s => s.ToHardwareCategoryDto());

            return Ok(hardwarecategoryDto);
        }

        [HttpGet]
        [Route("GetByIdHardwareCategories/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _hardwarecategoryRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToHardwareCategoryDto());
        }

        [HttpPost]
        [Route("CreateHardwareCategory")]
        public async Task<IActionResult> Create([FromBody] HardwareCategoryCreateDTO hardwarecategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool hardwareExists = await _hardwareRepo.ExistsAsync(hardwarecategoryDto.hardwareid);
            if (!hardwareExists)
            {
                return NotFound("Invalid Hardware ID.");
            }

            bool categoryExists = await _categoryRepo.ExistsAsync(hardwarecategoryDto.categoryid);
            if (!categoryExists)
            {
                return NotFound("Invalid Category ID.");
            }

            var hardwarecategoryModel = hardwarecategoryDto.ToHardwareCategoryFromCreateDTO();

            await _hardwarecategoryRepo.CreateAsync(hardwarecategoryModel);

            return Ok("Hardware Category Added");
        }

        [HttpDelete]
        [Route("DeleteHardwareCategory/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwarecategoryModel = await _hardwarecategoryRepo.DeleteAsync(id);

            if (hardwarecategoryModel == null)
            {
                return NotFound("No HardwareCategory found to delete.");
            }

            return NoContent();
        }
        [HttpPut]
        [Route("UpdateHardwareCategory/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] HardwareCategoryUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool hardwareExists = await _hardwareRepo.ExistsAsync(updateDto.hardwareid);
            if (!hardwareExists)
            {
                return NotFound("Invalid Hardware ID.");
            }

            bool categoryExists = await _categoryRepo.ExistsAsync(updateDto.categoryid);
            if (!categoryExists)
            {
                return NotFound("Invalid Category ID.");
            }

            var hardwarecategoryModel = await _hardwarecategoryRepo.UpdateAsync(id, updateDto);

            if (hardwarecategoryModel == null)
            {
                return NotFound("ID does not match any HardwareCategory");
            }

            return Ok("The HardwareCategory was updated!");
        }
    }
}