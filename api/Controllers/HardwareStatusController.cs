using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.HardwareStatusDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/hardwarecategory")]
    [ApiController]

    public class HardwareStatusController : ControllerBase
    {
        private readonly IHardwareStatus _hardwareStatus;
        public HardwareStatusController(IHardwareStatus hardwareStatus)
        {
            _hardwareStatus = hardwareStatus;
        }
        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<ActionResult<HardwareStatus>> GetById(int id)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hardwareStatus = await _hardwareStatus.GetHardwareStatusById(id);
            if (hardwareStatus == null)
            {
                return NotFound($"Hardware status with {id} not found");
            }
            return hardwareStatus;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<HardwareStatus>>> GetAll()
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _hardwareStatus.GetAllHardwareStatusesAsync();
        }
        [HttpPost]
        [Route("CreateHardwareStatus")]
        public async Task<ActionResult> Create([FromBody]HardwareStatusUpdateDTO hardwareStatus)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hardwareStatusModel = hardwareStatus.ToHardwareStatusFromCreate();

            await _hardwareStatus.CreateHardwareStatusAsync(hardwareStatusModel);

            return CreatedAtAction(nameof(GetById), new { id = hardwareStatusModel.id }, hardwareStatusModel);
        }
        [HttpPut]
        [Route("UpdateHardwareStatus/{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] HardwareStatusUpdateDTO hardwareStatus)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hardwareStatusModel = await _hardwareStatus.GetHardwareStatusById(id);
            if (hardwareStatusModel == null)
            {
                return NotFound($"Hardware status with {id} not found");
            }
            await _hardwareStatus.UpdateHardwareStatusAsync(id, hardwareStatus);
            return Ok("Hardware status upadted");
        }
        [HttpDelete]
        [Route("DeleteHardwareStatus/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hardwareStatusModel = await _hardwareStatus.GetHardwareStatusById(id);
            if (hardwareStatusModel == null)
            {
                return NotFound($"Hardware status with {id} not found");
            }
            await _hardwareStatus.DeleteHardwareStatusAsync(id);
            return Ok("Hardware status deleted");
        }
    }
}