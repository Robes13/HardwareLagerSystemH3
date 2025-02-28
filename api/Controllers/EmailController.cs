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
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IEmail _email;
        private readonly EmailCodeGenerator _createSecretEmailKey;
        private readonly EmailCodeSender _emailCodeSender;
        private readonly IUser _user;
        private readonly JwtTokenService _jwtTokenService; // Inject JwtTokenService

        public EmailController(ApiDbContext context, IEmail email, EmailCodeGenerator createSecretEmailKey, EmailCodeSender emailCodeSender, IUser user, JwtTokenService jwtTokenService)
        {
            _context = context;
            _email = email;
            _createSecretEmailKey = createSecretEmailKey;
            _emailCodeSender = emailCodeSender;
            _user = user;
            _jwtTokenService = jwtTokenService; // Initialize JwtTokenService
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateEmail([FromBody] CreateEmailDTO createEmailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = createEmailDTO.ToEmailFromCreateDTO();
            email.SecretKey = _createSecretEmailKey.GenerateSecretEmailKey();

            string emailBody = await ScrapeHtmlAndCss("https://sitemods.dk/apiprojekt/Email.html");

            if (string.IsNullOrEmpty(emailBody))
            {
                return StatusCode(500, "Failed to retrieve the email body.");
            }

            emailBody = emailBody.Replace("{emailcode}", email.SecretKey);

            // Send the verification email
            await _emailCodeSender.SendEmail(email.EmailAddress, "Bekræft din E-Mail på ITDepot", emailBody, true);

            // Try to create the email in the database
            try
            {
                email.SecretKey = BCrypt.Net.BCrypt.HashPassword(email.SecretKey);
                await _email.CreateEmailAsync(email);
            }
            catch (InvalidOperationException ex)
            {
                // If email already exists, return a conflict response
                return Conflict(ex.Message); // Return HTTP 409 Conflict with the error message
            }

            // Generate JWT Token after successful email creation
            var token = _jwtTokenService.GenerateToken(email.Id.ToString(), "User");

            // Return the created email along with the token
            var response = new
            {
                Email = email.ToEmailDTO(),
                Token = token
            };

            return CreatedAtAction(nameof(GetById), new { id = email.Id }, response);
        }

        private async Task<string> ScrapeHtmlAndCss(string url)
        {
            using HttpClient client = new HttpClient();

            try
            {
                // Fetch HTML
                string html = await client.GetStringAsync(url);

                // Extract CSS links
                MatchCollection matches = Regex.Matches(html, @"<link.*?href=['""](?<url>[^'""]+\.css)['""]");

                StringBuilder fullContent = new StringBuilder();
                fullContent.AppendLine(html); // Add the HTML first

                foreach (Match match in matches)
                {
                    string cssUrl = match.Groups["url"].Value;

                    // Convert relative URL to absolute if needed
                    if (!cssUrl.StartsWith("http"))
                    {
                        Uri baseUri = new Uri(url);
                        cssUrl = new Uri(baseUri, cssUrl).ToString();
                    }

                    try
                    {
                        string cssContent = await client.GetStringAsync(cssUrl);
                        fullContent.AppendLine($"<style>\n{cssContent}\n</style>");
                    }
                    catch (Exception)
                    {
                        // Ignore if CSS file fails to load
                    }
                }

                return fullContent.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
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

            // First, get the email by ID
            var email = await _email.GetByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            // Check if there is a user associated with the email
            var userWithEmail = await _user.GetByEmailAsync(id);
            if (userWithEmail != null)
            {
                // If a user is found with this email, return an error response
                return Conflict("Cannot delete this email, as it is associated with a user.");
            }

            // If no user is found, proceed with deleting the email
            var deletedEmail = await _email.DeleteAsync(id);
            if (deletedEmail == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("verifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] CheckEmailSecretCodeDTO updateVerifyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await _email.GetByEmailAsync(updateVerifyDTO.EmailAddress);

            if (email == null || !BCrypt.Net.BCrypt.Verify(updateVerifyDTO.SecretKey, email.SecretKey))
            {
                return Unauthorized("Invalid Email or password.");
            }

            await _email.UpdateVerify(updateVerifyDTO);
            if (email == null)
            {
                return NotFound();
            }
            return Ok(email.ToEmailDTO());
        }
    }
}