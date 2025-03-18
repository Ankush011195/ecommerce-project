<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signuppage.aspx.cs" Inherits="Ecommerce.Signuppage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
           <link rel="stylesheet" href="stylesheet\Signup.css" />

    <title></title>
</head>
<body>
        
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Create an Account</h2>

            <!-- Username Field -->
            <div class="input-container">
                <label for="username">Username</label>
                <asp:TextBox ID="Username" runat="server" CssClass="input-field" placeholder="Enter username" />

            </div>

            <!-- Email Field -->
            <div class="input-container">
                <label for="email">Email</label>
                <asp:TextBox ID="Email" runat="server" CssClass="input-field" placeholder="Enter email" />
            </div>

            <!-- Password Field -->
            <div class="input-container">
                <label for="password">Password</label>
                <asp:TextBox ID="Password" runat="server" CssClass="input-field" TextMode="Password" placeholder="Enter password" />
            </div>

            <!-- Sign-Up Button -->
            <div class="input-container">
                <asp:Button ID="SignUpButton" runat="server" Text="Sign Up" CssClass="signup-btn" OnClick="SignUpButton_Click" />
            </div>
        </div>
    </form>
    
</body>
</html>
