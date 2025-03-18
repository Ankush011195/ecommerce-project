using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Ecommerce.Utility
{
    public class EmailUtility
    {
        // Function to send the email
        public static bool SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                // Set up the SMTP client with Gmail's SMTP server (you can replace it with another SMTP provider)
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mehraankush188@gmail.com", "jtbfmoekwosqeygv"),
                    EnableSsl = true
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("mehraankush188@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Set to true if you want to send HTML content
                };
                mailMessage.To.Add(recipientEmail);

                // Send the email
                smtpClient.Send(mailMessage);

                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework if needed)
                Console.WriteLine("Error sending email: " + ex.Message);
                return false; // Failed to send email
            }
        }
    }
}


