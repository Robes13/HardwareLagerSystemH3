using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace api.Services
{
    public class EmailCodeSender
    {
        public async Task<bool> SendEmail(string mailAddress, string subject, string body, bool html)
        {// SMTP Server Configuration
            SmtpClient smtpClient = new SmtpClient("mail.itdepot.dk")
            {
                Port = 587, // Try this instead of 465
                EnableSsl = true, // For 587, some providers require EnableSsl = false and StartTLS enabled
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("system@itdepot.dk", "vlN?&*7?6pM?") // Replace with environment variables
            };


            // Email Options
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("system@itdepot.dk", "IT Depot System"),
                Subject = subject,
                Body = body,
                IsBodyHtml = html
            };

            mailMessage.To.Add(mailAddress); // Replace with recipient email

            try
            {
                // Send Email Asynchronously
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}