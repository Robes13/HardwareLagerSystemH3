using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.TypeDTOs;
using api.Interfaces;
using Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class TypeController : ControllerBase
    {

        /// <summary>
        /// IKKE FÆRDIGT LAV HURTIGST MULIGT!!!!!
        /// </summary>
        private readonly ITypes _typeRepo;
        public TypeController(ITypes typeRepo)
        {
            _typeRepo = typeRepo;
        }

        [HttpGet]
        [Route("GetAllTypes")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var types = await _typeRepo.GetAllAsync();

            var typeDto = types.Select(s => s.ToTypeDto());

            return Ok(typeDto);
        }

        [HttpGet("{id:int}")]
        [Route("GetByIdTypes")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var type = await _typeRepo.GetByIdAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return Ok(type.ToTypeDto());
        }

        [HttpPost]
        [Route("CreateType")]
        public async Task<IActionResult> Create([FromBody] TypeCreateDTO typeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeModel = typeDto.ToTypeFromCreate();

            await _typeRepo.CreateAsync(typeModel);

            return Ok("Type Added!");
        }

        [HttpPut]
        [Route("UpdateType/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TypeUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeModel = await _typeRepo.UpdateAsync(id, updateDto);

            if (typeModel == null)
            {
                return NotFound();
            }

            return Ok(typeModel.ToTypeDto());
        }
        [HttpDelete]
        [Route("DeleteType/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeModel = await _typeRepo.DeleteAsync(id);

            if (typeModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}