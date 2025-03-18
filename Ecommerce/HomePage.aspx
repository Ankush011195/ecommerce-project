<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Ecommerce.HomePage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <!-- Correct the link to Home.css -->
    <link rel="stylesheet" href="stylesheet/homepage.css" />

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
            <!-- Sidebar Section -->


            <!-- Main Content Section -->
            <div class="main-content">
                <div class="welcome-message-container">
                    <h1 class="welcome-message">Welcome to E-Store</h1>
                    <p>Your favorite online store for all tech products.</p>
                </div>

                <!-- Company List with One Product Each -->
                <div class="product-container">
                    <asp:Repeater ID="rptCompanies" runat="server">
                        <ItemTemplate>
                            <div class="product">
                                <img src='<%# Eval("ProductImage") %>' alt="Company Product" />
                                <h4><%# Eval("CompanyName") %></h4>
                                <a href="Products.aspx?company=<%# Eval("CompanyName") %>">
                                    <button class="view-details">View Products</button>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="testimonials">
                    <h2>What Our Customers Say</h2>
                    <asp:Repeater ID="rptTestimonials" runat="server">
                        <ItemTemplate>
                            <div class="testimonial">
                                <p>"<%# Eval("ReviewText") %>"</p>
                                <h4>- <%# Eval("CustomerName") %></h4>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="review-form">
            <h2>Leave a Review</h2>
            <asp:TextBox ID="txtName" runat="server" placeholder="Your Name"></asp:TextBox><br />
            <asp:TextBox ID="txtReview" runat="server" TextMode="MultiLine" placeholder="Your Review"></asp:TextBox><br />
            <asp:Button ID="btnSubmitReview" runat="server" Text="Submit Review" OnClick="SubmitReview_Click" />
        </div>


            </div>
        </div>


        <!-- Footer Section -->
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
