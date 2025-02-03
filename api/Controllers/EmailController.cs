using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using api.Mappers;
using api.DTOs.EmailDTOs;

namespace api.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IEmail _email;
        private readonly EmailCodeGenerator _createSecretEmailKey;
        private readonly EmailCodeSender _emailCodeSender;

        public EmailController(ApiDbContext context, IEmail email, EmailCodeGenerator createSecretEmailKey, EmailCodeSender emailCodeSender)
        {
            _context = context;
            _email = email;
            _createSecretEmailKey = createSecretEmailKey;
            _emailCodeSender = emailCodeSender;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await _email.GetByIdAsync(id);

            if (email == null)
            {
                return NotFound();
            }
            return Ok(email.ToEmailDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmail([FromBody] CreateEmailDTO createEmailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = createEmailDTO.ToEmailFromCreateDTO();
            email.SecretKey = _createSecretEmailKey.GenerateSecretEmailKey();

            // Path to your HTML file
            string emailBodyFilePath = @"C:\Users\zbc23rope\Desktop\Project Database Hjemmeside\Email\Email.html";

            // Step 4: Read the HTML file content as email body
            string emailBody = string.Empty;
            try
            {
                if (System.IO.File.Exists(emailBodyFilePath))
                {
                    emailBody = System.IO.File.ReadAllText(emailBodyFilePath);
                    emailBody = emailBody.Replace("{emailcode}", email.SecretKey);
                }
                else
                {
                    return NotFound("The email body HTML file was not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while reading the HTML email body.");
            }

            // Step 5: Send the email with the HTML content as body
            _emailCodeSender.SendEmail(email.EmailAddress, "Verify your email for TalentPortal", emailBody, true);

            // Step 6: Check if email already exists
            try
            {
                // Try to create the email in the database
                await _email.CreateEmailAsync(email);
            }
            catch (InvalidOperationException ex)
            {
                // If email already exists, return a conflict response
                return Conflict(ex.Message); // Return HTTP 409 Conflict with the error message
            }

            return CreatedAtAction(nameof(GetById), new { id = email.Id }, email.ToEmailDTO());
        }


        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> UpdateEmail([FromRoute] int id, [FromBody] EmailUpdateDTO emailUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await _email.UpdateAsync(id, emailUpdate);

            if (email == null)
            {
                return NotFound();
            }

            return Ok(email.ToEmailDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = await _email.DeleteAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut]
        [Route("verifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] CheckEmailSecretCodeDTO updateVerifyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = await _email.UpdateVerify(updateVerifyDTO);
            if (email == null)
            {
                return NotFound();
            }
            return Ok(email.ToEmailDTO());
        }
    }
}