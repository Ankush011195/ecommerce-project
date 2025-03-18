<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Landingpage.aspx.cs" Inherits="Ecommerce.Landingpage"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="stylesheet/Landing.css" / />
    
    <title></title>
</head>
<body>
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

    <!-- Landing Page Main Content -->
    <div class="background-image">
        <!-- Transparent Box in the Center -->
        <div class="center-box">
            <h1>Biggest Electronics Store</h1>

            <!-- Form with a button to redirect to the HomePage -->
            <form action="HomePage.aspx" method="get">
                <button type="submit" class="show-now-btn">Show Now</button>
            </form>
        </div>
    </div>
     <!-- Footer Section -->
 <footer>
     <div class="footer-links">
         <a href="Terms.aspx">Terms & Conditions</a>
         <a href="Privacy.aspx">Privacy Policy</a>
         <a href="Contact.aspx">Contact Us</a>
     </div>
     <p>&copy; 2025 Ecommerce. All Rights Reserved.</p>
 </footer>
</body>
</html>
