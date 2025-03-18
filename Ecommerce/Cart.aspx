<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Ecommerce.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shopping Cart</title>
    <link rel="stylesheet" href="stylesheet/Cart.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>Shopping Cart</h1>
        </header>

        <div class="container">
            <!-- Display the Cart Items -->
            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand" CssClass="cart-table">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />

                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                   <asp:Button runat="server" Text="Remove" 
                    CommandName="Remove" 
                    CommandArgument='<%# Eval("ProductID") %>' 
                    CssClass="remove-btn" />                       
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Show message if cart is empty -->
            <asp:Label ID="lblEmptyCart" runat="server" Text="Your cart is empty." CssClass="empty-cart-message" Visible="false"></asp:Label>

            <!-- Checkout Button -->
            <div class="cart-actions">
                <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="checkout-btn" OnClick="btnCheckout_Click" />
            </div>
        </div>
    </form>
</body>
</html>
