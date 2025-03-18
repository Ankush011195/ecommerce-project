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
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProductCategories();// Load categories into dropdown
                LoadProducts();    // Load all products initially
            }
        }

        // 🔹 Load Categories into Dropdown
        private void LoadProductCategories()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT CategoryID, CategoryName FROM ProductCategories"; // Use correct table name

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptCategories.DataSource = dt;
                    rptCategories.DataBind();
                }
            }
        }
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT CategoryID, CategoryName FROM Categories"; // Use correct table name

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "CategoryName"; 
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataBind();
                }
            }
        }

        // 🔹 Load Products from Database
        private void LoadProducts(string searchQuery = "", int categoryId = 0, int productsCategoryId = 0)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT ProductID, ProductName, Price, ImageUrl FROM Products WHERE IsActive = 1";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " AND ProductName LIKE @Search";
                }
                if (categoryId > 0)
                {
                    query += " AND CategoryID = @CategoryID";
                }
                if (productsCategoryId > 0)
                {
                    query += " AND ProductCategoryId = @ProductCategoryId";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                }
                if (categoryId > 0)
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                }
                if (productsCategoryId > 0)
                {
                    cmd.Parameters.AddWithValue("@ProductCategoryId", productsCategoryId);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
        }

        // 🔹 Search Products
        protected void SearchProducts(object sender, EventArgs e)
        {
            string searchQuery = Request.Form["txtSearch"]; // Get search value from input field
            LoadProducts(searchQuery);
        }

        // 🔹 Filter by Category
        protected void FilterByCategory(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            LoadProducts("", categoryId, 0);
        }

        protected void Category_Click(object sender, EventArgs e)
        {
            LinkButton btnCategory = (LinkButton)sender;
            int productsCategoryId = Convert.ToInt32(btnCategory.CommandArgument);
            LoadProducts("", 0, productsCategoryId);  // Load products for selected category
        }

        // 🔹 Add to Cart Functionality
       
    }

}
