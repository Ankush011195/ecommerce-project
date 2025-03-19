<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellPhone.aspx.cs" Inherits="Ecommerce.SellPhone" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="stylesheet/SellPhone.css" />

</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="logo">
                <a href="HomePage.aspx">E-Store.com</a>
            </div>
            <nav class="navbar">
                <ul class="nav-links">
                    <li><a href="Contact.aspx">Contact</a></li>
                    <li><a href="About.aspx">About</a></li>
                    <li><a href="SellPhone.aspx">Sell Your Phone</a></li>
                    <li><a href="Products.aspx">Products</a></li>
                    <li><a href="HomePage.aspx">Home</a></li>
                    <li><a href="Loginpage.aspx">Login</a></li>
                </ul>
            </nav>
        </header>

        <div class="container">
            <div class="main-content">
                <h1>Sell Your Phone</h1>
                <p>Fill in the details below to sell your phone.</p>

                <div class="sell-form">
                    <label for="txtName">Your Name:</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="input-box"></asp:TextBox><br />

                    <label for="txtEmail">Your Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input-box"></asp:TextBox><br />

                    <label for="txtPhoneModel">Phone Model:</label>
                    <asp:TextBox ID="txtPhoneModel" runat="server" CssClass="input-box"></asp:TextBox><br />

                    <label for="txtDescription">Description:</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="input-box" TextMode="MultiLine"></asp:TextBox><br />

                    <label for="fuImage">Upload Image:</label>
                    <asp:FileUpload ID="fuImage" runat="server" /><br />

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submit-button" OnClick="btnSubmit_Click" /><br />

                    <!-- Message Label (Fix for lblMessage) -->
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>

            </div>
        </div>

        <footer>
            <div class="footer-links">
                <a href="Terms.aspx">Terms & Conditions</a>
                <a href="Privacy.aspx">Privacy Policy</a>
                <a href="Contact.aspx">Contact Us</a>
            </div>
            <p>&copy; 2025 E-Store. All Rights Reserved.</p>
        </footer>
    </form>
</body>
</html>
