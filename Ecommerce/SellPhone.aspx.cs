using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
	public partial class SellPhone : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phoneModel = txtPhoneModel.Text;
            string description = txtDescription.Text;

            if (fuImage.HasFile)
            {
                string imagePath = "~/uploads/" + fuImage.FileName;
                fuImage.SaveAs(Server.MapPath(imagePath));

                // Here, you can save the details into the database (if needed)
                lblMessage.Text = "Your phone details have been submitted successfully!";
            }
            else
            {
                lblMessage.Text = "Please upload an image of your phone.";
            }
        }
    }
}