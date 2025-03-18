using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Ecommerce
{
    public partial class Loginpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        // Event handler for the Login button click
        public void LoginButton_Click(object sender, EventArgs e)
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            password = HashPassword(password);

            // Check if the username exists
            if (DoesUsernameExist(username))
            {
                // Proceed with password validation
                if (AuthenticateUser(username, password))
                {
                    // Redirect user to another page on successful login
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    // Show error message if password is incorrect
                    Response.Write("<script>alert('Invalid Username or Password');</script>");
                }
            }
            else
            {
                // Show error message if username does not exist
                Response.Write("<script>alert('Username does not exist');</script>");
            }
        }

        // Method to check if the username exists in the database
        private bool DoesUsernameExist(string username)
        {
            // Connection string from web.config
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // SQL query to check if the username exists
                string query = "SELECT COUNT(*) FROM Customer WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Prevent SQL Injection by using parameterized queries
                    cmd.Parameters.AddWithValue("@Username", username);

                    // Execute the query and return whether the username exists
                    int userCount = (int)cmd.ExecuteScalar();
                    return userCount > 0; // True if username exists, false otherwise
                }
            }
        }

        // Method to authenticate user against the database (check username and password)
        private bool AuthenticateUser(string username, string password)
        {
            // Connection string from web.config
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // SQL query to check if the username and password match
                string query = "SELECT UserID FROM Customer WHERE Username = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Prevent SQL Injection by using parameterized queries
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Execute the query and return whether the user exists
                    object userId = cmd.ExecuteScalar();
                    if (userId != null)
                    {
                        Session["UserID"] = userId.ToString(); // Ensure it's stored as a string or integer
                        return true;
                    }
                    else
                        return false;
                }
            }
        }
    }
}
