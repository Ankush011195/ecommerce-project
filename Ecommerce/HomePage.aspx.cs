using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadCompanies();
            }
            if (!IsPostBack)
            {
                LoadTestimonials();
            }
        }
        private void LoadCompanies()
        {
            string connString = ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                SELECT p.CompanyName, p.ImageURL AS ProductImage
                FROM Products p
                WHERE p.ProductID IN 
                (SELECT TOP 1 p1.ProductID FROM Products p1 
                 WHERE p1.CompanyName = p.CompanyName 
                 ORDER BY p1.DateAdded DESC)
                AND p.IsActive = 1;";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptCompanies.DataSource = dt;
                    rptCompanies.DataBind();
                }
            }
        }
        private void LoadTestimonials()
        {
            string connStr = ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 5 CustomerName, ReviewText FROM CustomerReviews ORDER BY DateAdded DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptTestimonials.DataSource = reader;
                    rptTestimonials.DataBind();
                }
            }
        }
        protected void SubmitReview_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO CustomerReviews (CustomerName, ReviewText) VALUES (@CustomerName, @ReviewText)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", txtName.Text);
                    cmd.Parameters.AddWithValue("@ReviewText", txtReview.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Reload testimonials to show the new review
            LoadTestimonials();
        }

    }
}