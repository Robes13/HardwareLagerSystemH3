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
        #region  Fields
        const string HOSTNAME="smtp.gmail.com";
        const int PORT=587;
        const string USERNAME="automechanicsapp@gmail.com";
        const string PASSWORD="rrta rwwn cflz hylc";
        const bool ENABLESSL = true;
        #endregion

        public bool SendEmail(string mailAddress, string subject, string body, bool isBodyHtml)
        {
            try
            {
                string senderEmail = USERNAME; 
                string senderPassword = PASSWORD;

                SmtpClient smtpClient = new SmtpClient(HOSTNAME, PORT)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = ENABLESSL 

                };
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml 
                };

                // Add recipients
                mailMessage.To.Add(mailAddress);

                // Send the email
                smtpClient.Send(mailMessage);

                Console.WriteLine("Email sent successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }
    }
}