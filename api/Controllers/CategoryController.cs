using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.CategoryDTOs;
using api.Interfaces;
using Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepo;
        public CategoryController(ICategory categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categories = await _categoryRepo.GetAllAsync();

            var categoryDto = categories.Select(s => s.ToCategoryDto());

            return Ok(categoryDto);
        }

        [HttpGet("{id:int}")]
        [Route("GetByIdCategories")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
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
                return NotFound();
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
                return NotFound();
            }

            return NoContent();
        }

    }
}