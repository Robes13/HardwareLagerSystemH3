using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.CategoryDTOs;
using api.Helpers.QueryObjects;
using api.Interfaces;
using Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepo;
        public CategoryController(ICategory categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAll([FromQuery] CategoryQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categories = await _categoryRepo.GetAllAsync(query);

            var categoryDto = categories.Select(s => s.ToCategoryDto());

            return Ok(categoryDto);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetByIdCategories/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound("No Category found with that ID.");
            }

            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = categoryDTO.ToCategoryFromCreate();

            await _categoryRepo.CreateAsync(categoryModel);

            return Ok("Category Added!");
        }

        [HttpPut]
        [Route("UpdateCategory/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = await _categoryRepo.UpdateAsync(id, updateDto);

            if (categoryModel == null)
            {
                return NotFound("No Category found to update");
            }

            return Ok(categoryModel.ToCategoryDto());
        }
        [HttpDelete]
        [Route("DeleteCategory/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = await _categoryRepo.DeleteAsync(id);

            if (categoryModel == null)
            {
                return NotFound("No Category found to delete");
            }

            return NoContent();
        }

    }
}