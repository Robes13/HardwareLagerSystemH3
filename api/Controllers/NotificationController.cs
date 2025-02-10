using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.NotificationDTOs;
using api.Helpers.QueryObjects;
using api.Interfaces;
using Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("api/type")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notiRepo;
        public NotificationController(INotification notiRepo)
        {
            _notiRepo = notiRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] NotificationQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notifications = await _notiRepo.GetAllAsync(query);

            var notificationDto = notifications.Select(s => s.ToNotificationDto());

            return Ok(notificationDto);
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notification = await _notiRepo.GetByIdAsync(id);

            if (notification == null)
            {
                return NotFound("No Notification found with that ID.");
            }

            return Ok(notification.ToNotificationDto());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] NotificationCreateDTO notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notificationModel = notificationDto.ToNotificationFromCreate();

            await _notiRepo.CreateAsync(notificationModel);

            return Ok("Notification Added!");
        }
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notificationModel = await _notiRepo.DeleteAsync(id);

            if (notificationModel == null)
            {
                return NotFound("No Notification found to delete.");
            }

            return NoContent();
        }
    }
}