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
            if (!IsPostBack)  // Prevents resetting dropdown on postback
            {
                LoadCategories();
                LoadProductCategories();
                LoadProducts();
            }
        }

        // 🔹 Load Categories into Dropdown
        private void LoadProductCategories()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT CategoryID, CategoryName FROM ProductCategories";

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
                string query = "SELECT CategoryID, CategoryName FROM Categories";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Add "All Categories" option
                    DataRow dr = dt.NewRow();
                    dr["CategoryID"] = 0;
                    dr["CategoryName"] = "All Categories";
                    dt.Rows.InsertAt(dr, 0);

                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "CategoryName";
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataBind();
                }
            }
        }

        // 🔹 Load Products from Database
        private void LoadProducts(string searchQuery = "", int categoryId = 0, int productsCategoryId = 0, int maxPrice=0)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT ProductID, ProductName, Price, ImageUrl, CategoryID FROM Products WHERE IsActive = 1";

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
                if (maxPrice > 0)
                {
                    query += " AND Price <= @Price";
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
                if (maxPrice > 0)
                {
                    cmd.Parameters.AddWithValue("@Price", maxPrice);
                }

                // Debugging: Print query in browser console
                string debugQuery = cmd.CommandText;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    debugQuery = debugQuery.Replace(p.ParameterName, p.Value.ToString());
                }
              //  Response.Write("<script>console.log('Query: " + debugQuery + "');</script>");

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
            string searchQuery = txtSearch.Text.Trim();
            int categoryId = 0;

            if (ddlCategory.SelectedIndex > -1 && int.TryParse(ddlCategory.SelectedValue, out int selectedCategoryId))
            {
                categoryId = selectedCategoryId;
            }

            // Debugging: Show category ID in an alert box
            Response.Write("<script>alert('Selected Category ID: " + categoryId + "');</script>");

            LoadProducts(searchQuery, categoryId, 0);
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
            LoadProducts("", 0, productsCategoryId);
        }

        //protected void FilterByPrice(object sender, EventArgs e)
        //{
        //    int maxPrice = 9999999; // Default max price

        //    if (!string.IsNullOrEmpty(hfPrice.Value) && int.TryParse(hfPrice.Value, out int selectedPrice))
        //    {
        //        maxPrice = selectedPrice;
        //    }
        //    LoadProducts("", Convert.ToInt32(ddlCategory.SelectedValue), 0, maxPrice);
        //}
        protected void FilterByPrice(object sender, EventArgs e)
        {
            int maxPrice = 0;
            maxPrice=Convert.ToInt32( priceRange.Value);
            LoadProducts("", Convert.ToInt32(ddlCategory.SelectedValue), 0, maxPrice);

        }


    }
}
