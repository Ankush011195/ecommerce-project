using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
	public partial class Contact : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // Ensures required fields are filled
            {
                try
                {
                    // Configure SMTP settings
                    SmtpClient smtpClient = new SmtpClient("smtp.your-email-provider.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
                        EnableSsl = true
                    };

                    // Create email message
                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("your-email@example.com"),
                        Subject = txtSubject.Text,
                        Body = $"Name: {txtName.Text}\nEmail: {txtEmail.Text}\nMessage:\n{txtMessage.Text}",
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add("your-email@example.com"); // Replace with your actual email

                    // Send the email
                    smtpClient.Send(mailMessage);

                    // Display success message
                    lblMessage.Text = "Your message has been sent successfully!";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtSubject.Text = "";
                    txtMessage.Text = "";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error sending message: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}