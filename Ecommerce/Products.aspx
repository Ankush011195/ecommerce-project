<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Ecommerce.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="stylesheet/Products.css"  />
</head>
<body>
     <form id="form1" runat="server">
     <header>
        <h1>Our Products</h1>
        <div class="search-bar">
            <input type="text" id="txtSearch" placeholder="Search products..." />
            <button id="btnSearch" runat="server" onserverclick="SearchProducts">Search</button>
        </div>
    </header>
    
        <div class="container">
            <!-- Sidebar for Filtering -->
            <aside class="sidebar">
                <h3>Product Categories</h3>
                <ul class="category-list">
    <asp:Repeater ID="rptCategories" runat="server">
        <ItemTemplate>
            <li>
                <asp:LinkButton runat="server" CommandArgument='<%# Eval("CategoryID") %>' OnClick="Category_Click">
                    <%# Eval("CategoryName") %>
                </asp:LinkButton>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>


                <h3>Filter by Category</h3>
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterByCategory"></asp:DropDownList>

                <h3>Filter by Price</h3>
                <input type="range" id="priceRange" min="0" max="1000" step="10" />
                <span id="priceValue">$0 - $1000</span>
            </aside>
            <asp:Label ID="lblNoProducts" runat="server" Text="No products found." CssClass="no-products" Visible="false"></asp:Label>
            <!-- Main Product Display Section -->
            <section class="product-section">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="product">
                            <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("ProductName") %>'>
                            <h4><%# Eval("ProductName") %></h4>
                            <p>Price: $<%# Eval("Price") %></p>
                       <%--<asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" 
                        CommandArgument='<%# Eval("ProductID") %>' 
                        OnClick="AddToCart_Click" CssClass="add-to-cart" />--%>
                    <a href='Cart.aspx?ProductID=<%# Eval("ProductID") %>' class="add-to-cart-button">Add to Cart</a>




                              <!-- Buy Now Button -->
                            <button class="buy-now-button" onclick="buynow('<%# Eval("ProductID") %>')">Buy now</button>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </section>
        </div>
     </form>
    
    <script>
        document.getElementById("priceRange").oninput = function () {
            document.getElementById("priceValue").innerText = "$0 - $" + this.value;
        };

    </script>
</body>
</html>
