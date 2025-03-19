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
                upiSection.Visible = false; // Hide UPI section initially
            }
        }

        private void LoadCart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            gvCart.DataSource = cart;
            gvCart.DataBind();

            if (cart.Count > 0)
            {
                decimal totalAmount = cart.Sum(item => item.Price * item.Quantity);
                lblTotal.Text = totalAmount.ToString("C");
            }
            else
            {
                lblTotal.Text = "₹0.00";
            }
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
            else
            {
                lblMessage.Text = "Please fill in all required fields.";
            }
        }

        private bool ValidateForm()
        {
            bool isValid = !string.IsNullOrWhiteSpace(txtFullName.Text) &&
                           !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                           !string.IsNullOrWhiteSpace(txtPhone.Text) &&
                           !string.IsNullOrWhiteSpace(txtAddress.Text);

            // If UPI is selected, ensure Transaction ID is provided
            if (rblPaymentMethod.SelectedValue == "UPI")
            {
                isValid &= !string.IsNullOrWhiteSpace(txtUPITransactionID.Text);
            }

            return isValid;
        }

        protected void chkSameAsBilling_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsBilling.Checked)
            {
                txtShippingAddress.Text = txtAddress.Text;
                txtShippingAddress.Enabled = false;
            }
            else
            {
                txtShippingAddress.Text = "";
                txtShippingAddress.Enabled = true;
            }
        }

        protected void rblPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            upiSection.Visible = (rblPaymentMethod.SelectedValue == "UPI");
        }
    }
}
