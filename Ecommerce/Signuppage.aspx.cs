using Ecommerce.Utility;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce
{
    public partial class Signuppage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any logic to execute when the page loads (if needed)
        }

        protected void SignUpButton_Click(object sender, EventArgs e)
        {
            // Collect user input from the form
            string username = Request.Form["username"];
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            // Hash the password before saving it
            string hashedPassword = HashPassword(password);

            // Insert user into the database
            if (RegisterUser(username, email, hashedPassword))
            {
                // Redirect to login page after successful registration
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                // Display error message if registration fails
                Response.Write("<script>alert('Registration failed. Try again.');</script>");
            }
        }

        // Method to insert user into the database
        private bool RegisterUser(string username, string email, string password)
        {
            // Retrieve connection string from the web.config file
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // SQL query to insert the new user
                string query = "INSERT INTO  Customer(Username, Email, Password) VALUES (@Username, @Email, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Random random = new Random();

                    string otp = random.Next(10000, 99999).ToString(); // OTP value, can be dynamically generated
                    string emailBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <title>OTP Verification</title>
                        <style>
                            body {{ font-family: Arial, sans-serif; }}
                            .email-container {{ background-color: #f4f4f4; padding: 20px; text-align: center; }}
                            .otp-box {{ background-color: #fff; padding: 20px; border-radius: 8px; border: 1px solid #ccc; }}
                            .otp-code {{ font-size: 24px; font-weight: bold; color: #2d9cdb; }}
                            .footer {{ font-size: 12px; color: #777; margin-top: 20px; }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <h2>OTP Verification</h2>
                            <p>Dear User,</p>
                            <p>We received a request to verify your account. Use the following OTP code to complete the process:</p>
                            <div class='otp-box'>
                                <p class='otp-code'>{otp}</p>
                            </div>
                            <p>This OTP code is valid for the next 10 minutes. If you didn't request this, please ignore this email.</p>
                            <div class='footer'>
                                <p>Thank you for using E-Store.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                    ";
                    bool emailCheck = EmailUtility.SendEmail(email,"Otp Verification mail", emailBody);
                    return rowsAffected > 0; // Return true if the registration was successful
                }
            }
        }

        // Method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
