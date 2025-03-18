<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Ecommerce.Contact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact us</title>
      <link rel="stylesheet" href="stylesheet/Contact.css" />
</head>
<body>
    <form id="form1" runat="server">
         <div class="container">
            <h2>Contact Us</h2>

            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>

            <div>
                <label>Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
               
                   
            </div>

            <div>
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
               
               
            </div>

            <div>
                <label>Subject:</label>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                
            </div>

            <div>
                <label>Message:</label>
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
               
            </div>

            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
