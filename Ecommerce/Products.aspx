<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Ecommerce.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="stylesheet/Products.css"  />
</head>
<body>
     <form id="form1" runat="server">
          <h1>Our Products</h1>
     <header>
          <a href="javascript:history.back();" class="back-arrow">&#8592;</a>
       
        <div class="search-bar">
    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search products..."></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="search-button" OnClick="SearchProducts" />
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

                <%--<h3>Filter by Price</h3>
                <input type="range" id="priceRange" min="0" max="1000" step="10" />
                <span id="priceValue">$0 - $1000</span>--%>
                <h3>Filter by Price</h3>
<input type="range" id="priceRange" min="0" max="10000" step="10" runat="server" />
<span id="priceValue">$0 - $1000</span>
<asp:HiddenField ID="hfPrice" runat="server" />

<asp:Button ID="btnFilterPrice" runat="server" Text="Apply Price Filter" CssClass="filter-button" OnClick="FilterByPrice" />

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
