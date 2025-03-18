<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Ecommerce.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>checkout</title>
     <link rel="stylesheet" href="stylesheet/Checkout.css" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <h2>🛒 Checkout</h2>

            <!-- Billing Details -->
            <fieldset class="section">
                <legend>📌 Billing Details</legend>
                <div class="form-group">
                    <label>Full Name:</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Enter your full name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Phone:</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Enter your phone number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Address:</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Placeholder="Enter your address"></asp:TextBox>
                </div>
            </fieldset>

            <!-- Shipping Details -->
            <fieldset class="section">
                <legend>🚚 Shipping Details</legend>
                <asp:CheckBox ID="chkSameAsBilling" runat="server" Text="Shipping address same as billing?" AutoPostBack="true" OnCheckedChanged="chkSameAsBilling_CheckedChanged" CssClass="checkbox-label" />
                <div id="shippingDetails" runat="server">
                    <div class="form-group">
                        <label>Shipping Address:</label>
                        <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="true" Placeholder="Enter shipping address"></asp:TextBox>
                    </div>
                </div>
            </fieldset>

            <!-- Order Summary -->
            <fieldset class="section">
                <legend>📦 Order Summary</legend>
                <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="cart-table">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    </Columns>
                </asp:GridView>
            </fieldset>

            <!-- Coupon Code -->
            <div class="coupon-section">
                <label>🎟️ Coupon Code:</label>
                <asp:TextBox ID="txtCoupon" runat="server" CssClass="form-control" Placeholder="Enter coupon code"></asp:TextBox>
                <asp:Button ID="btnApplyCoupon" runat="server" Text="Apply Coupon" CssClass="btn-secondary" />
            </div>

            <!-- Payment Options -->
            <fieldset class="section">
                <legend>💳 Payment Method</legend>
                <asp:RadioButtonList ID="rblPaymentMethod" runat="server" CssClass="radio-list">
                    <asp:ListItem Value="COD" Selected="True">Cash on Delivery</asp:ListItem>
                    <asp:ListItem Value="CreditCard">Credit/Debit Card</asp:ListItem>
                    <asp:ListItem Value="UPI">UPI / Bank Transfer</asp:ListItem>
                </asp:RadioButtonList>
            </fieldset>

            <!-- Final Order Review -->
            <div class="total-section">
                <h3>Total: <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label></h3>
            </div>

            <!-- Place Order Button -->
            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn-primary" />

            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        </form>
    </div>
</body>
</html>
