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

        public HardwareCategoryController(ApiDbContext context, IHardwareCategory hardwarecategoryRepo)
        {
            _context = context;

            _hardwarecategoryRepo = hardwarecategoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] HardwareCategoryQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwarecategory = await _hardwarecategoryRepo.GetAllAsync(query);

            var hardwarecategoryDto = hardwarecategory.Select(s => s.ToHardwareCategoryDto());

            return Ok(hardwarecategory);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _hardwarecategoryRepo.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToHardwareCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HardwareCategoryCreateDTO hardwarecategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hardwarecategoryModel = hardwarecategoryDto.ToHardwareCategoryFromCreateDTO();

            await _hardwarecategoryRepo.CreateAsync(hardwarecategoryModel);

            return Ok("Hardware Category Added");
        }
    }
}