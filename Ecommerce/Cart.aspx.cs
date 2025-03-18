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
	public partial class Cart : System.Web.UI.Page
	{
        int productId;
        string productName;
        decimal price;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["productId"] != null)
                {
                    if (int.TryParse(Request.QueryString["productId"], out productId))
                    {
                        AddToCart(productId);
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid Product ID');</script>");
                    }
                }
                LoadCart();
            }
        }

        private void AddToCart(int productId)
        {
            string connString = ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            int userId =Convert.ToInt32( Session["UserID"] != null ? Session["UserID"]: 0);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();


                    // Retrieve product details
                    string selectQuery = "SELECT ProductName, Price FROM Products WHERE ProductID = @ProductID";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@ProductID", productId);
                        SqlDataReader reader = selectCmd.ExecuteReader();

                    if (reader.Read()) // If product found
                    {
                        productName = reader["ProductName"].ToString();
                        price = Convert.ToDecimal(reader["Price"]);
                        reader.Close();
                        if (userId > 0)
                        {
                            // Check if the product already exists in the cart
                            string checkQuery = "SELECT Quantity FROM Cart WHERE UserID = @UserID AND ProductID = @ProductID";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@UserID", userId);
                                checkCmd.Parameters.AddWithValue("@ProductID", productId);
                                object existingQuantity = checkCmd.ExecuteScalar();

                                if (existingQuantity != null) // Product exists, update quantity
                                {
                                    int newQuantity = Convert.ToInt32(existingQuantity) + 1;
                                    string updateQuery = "UPDATE Cart SET Quantity = @Quantity WHERE UserID = @UserID AND ProductID = @ProductID";
                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@Quantity", newQuantity);
                                        updateCmd.Parameters.AddWithValue("@UserID", userId);
                                        updateCmd.Parameters.AddWithValue("@ProductID", productId);
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                                else // Product not in cart, insert new entry
                                {
                                    string insertQuery = "INSERT INTO Cart (UserID, ProductID, ProductName, Price, Quantity, CreatedAt) " +
                                                         "VALUES (@UserID, @ProductID, @ProductName, @Price, @Quantity, @CreatedAt)";
                                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@UserID", userId);
                                        insertCmd.Parameters.AddWithValue("@ProductID", productId);
                                        insertCmd.Parameters.AddWithValue("@ProductName", productName);
                                        insertCmd.Parameters.AddWithValue("@Price", price);
                                        insertCmd.Parameters.AddWithValue("@Quantity", 1); // Default to 1 on first insert
                                        insertCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        } else
                    {
                        AddCartSession(productId, productName, price, 1);
                    }
                    }
                   
                        else
                        {
                            // Product not found, handle error
                            Response.Write("<script>alert('Product not found!');</script>");
                        }
                    }
                
                

                    conn.Close();
            }
        }

        private void AddCartSession(int productId, string productName, decimal price, int quantity)
        {
            //// Initialize session cart if it's null
            //if (Session["Cart"] == null)
            //{
            //    Session["Cart"] = new DataTable();
            //    DataTable cart = (DataTable)Session["Cart"];
            //    cart.Columns.Add("ProductID", typeof(int));
            //    cart.Columns.Add("ProductName", typeof(string));
            //    cart.Columns.Add("Price", typeof(decimal));
            //    cart.Columns.Add("Quantity", typeof(int));
            //    cart.Columns.Add("TotalPrice", typeof(decimal));
            //}

            //// Retrieve cart from session
            //DataTable dtCart = (DataTable)Session["Cart"];

            //// Check if the product already exists in the cart
            //DataRow existingProduct = dtCart.AsEnumerable().FirstOrDefault(row => row.Field<int>("ProductID") == productId);
            //if (existingProduct != null)
            //{
            //    // Update quantity and total price
            //    existingProduct["Quantity"] = Convert.ToInt32(existingProduct["Quantity"]) + quantity;
            //    existingProduct["TotalPrice"] = Convert.ToDecimal(existingProduct["Price"]) * Convert.ToInt32(existingProduct["Quantity"]);
            //}

            //else
            //{
            //    // Add new product to cart
            //    DataRow newRow = dtCart.NewRow();
            //    newRow["ProductID"] = productId;
            //    newRow["ProductName"] = productName;
            //    newRow["Price"] = price;
            //    newRow["Quantity"] = quantity;
            //    newRow["TotalPrice"] = price * quantity;
            //    dtCart.Rows.Add(newRow);
            //}

            //// Save cart back to session
            //Session["Cart"] = dtCart;

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            CartItem existingItem = cart.Find(p => p.ProductID == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem { ProductID = productId, ProductName = productName, Price = price, Quantity = quantity });
            }

            Session["Cart"] = cart;
            //lblMessage.Text = "Product added to cart!";
        }
       


        private void LoadCart()
        {
            string connString = ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            int userId = Convert.ToInt32( Session["UserID"]!=null?Session["UserID"]:0);
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // Check if session cart exists
            if (userId <= 0)
            {
                if (Session["Cart"] != null)
                {
                    gvCart.DataSource = cart;
                    gvCart.DataBind();
                }
                else
                {
                    gvCart.DataSource = null;
                    gvCart.DataBind();
                    // lblMessage.Text = "Your cart is empty."; // Display message if cart is empty
                }


                // Bind cart data to GridView

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT CartID,ProductID, ProductName, Price, Quantity, (Price * Quantity) AS TotalAmount FROM Cart WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();

                    gvCart.DataSource = dt;
                    gvCart.DataBind();

                    decimal totalAmount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["TotalAmount"]);
                    }
                    // lblTotalAmount.Text = "Total: " + totalAmount.ToString("C");
                }
            }
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["EcommerceDB"].ConnectionString;
            int userId = Convert.ToInt32(Session["UserID"] != null ? Session["UserID"] : 0);

            if (e.CommandName == "Remove")
            {
                if (userId > 0)
                {
                    int productId = Convert.ToInt32(e.CommandArgument);
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        string query = "DELETE FROM Cart WHERE ProductID = @ProductID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                else
                {
                    int productId = Convert.ToInt32(e.CommandArgument);

                    List<CartItem> cart = Session["Cart"] as List<CartItem>;

                    if (cart != null)
                    {
                        cart.RemoveAll(item => item.ProductID == productId);
                        Session["Cart"] = cart;
                    }
                }
                    LoadCart();
            }
            else if (e.CommandName == "UpdateQuantity")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int cartId = Convert.ToInt32(e.CommandArgument);
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                int newQuantity = Convert.ToInt32(txtQuantity.Text);

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "UPDATE Cart SET Quantity = @Quantity WHERE CartID = @CartID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    cmd.Parameters.AddWithValue("@CartID", cartId);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                LoadCart();
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }
    }
}
	