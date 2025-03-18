<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginpage.aspx.cs" Inherits="Ecommerce.Loginpage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="stylesheet/login.css" />
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ParentDiv">
            <div>
                <h2>Sign up to inbox</h2>
                
                <!-- Using ASP.NET controls for username and password input -->
                <div class="input-container">
                    <label for="Username">Username</label>
                    <asp:TextBox ID="Username" runat="server" CssClass="input-field" />
                </div>

                <div class="input-container">
                    <label for="Password">Password</label>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="input-field" />
                </div>

                <!-- ASP.NET Button control to trigger login -->
                <asp:Button ID="LoginButton" runat="server" CssClass="login-btn" Text="Login" OnClick="LoginButton_Click" />

                <!-- Link to sign-up page -->
                <div class="signup-link">
                    <p>Don't have an account? <a href="Signuppage.aspx">Sign Up</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
