using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        private void LoadCart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            gvCart.DataSource = cart;
            gvCart.DataBind();

            lblTotal.Text = cart.Sum(item => item.Price * item.Quantity).ToString("C");
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                List<CartItem> cart = Session["Cart"] as List<CartItem>;

                if (cart != null && cart.Count > 0)
                {
                    // Process order logic here
                    Session["Cart"] = null;
                    Response.Redirect("OrderConfirmation.aspx");
                }
                else
                {
                    lblMessage.Text = "Your cart is empty!";
                }
            }
        }

        private bool ValidateForm()
        {
            return !string.IsNullOrWhiteSpace(txtFullName.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtPhone.Text) &&
                   !string.IsNullOrWhiteSpace(txtAddress.Text);
        }

        protected void chkSameAsBilling_CheckedChanged(object sender, EventArgs e)
        {
           if(chkSameAsBilling.Checked)
            {
                txtShippingAddress.Enabled = false;
            }
            else
            {
                txtShippingAddress.Enabled = true;

            }
        }
    }
   
        
    


}