namespace Ecommerce
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }  // 🔹 Add this property
        public int Quantity { get; set; }
    }
}