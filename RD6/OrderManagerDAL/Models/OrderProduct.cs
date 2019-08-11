namespace OrderManagerDAL.Models
{
    /// <summary>
    /// Represents a MANY-MANY link entity between 'Order' and 'Product'
    /// </summary>
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order OrderNav { get; set; }

        public string ProductGTIN { get; set; }
        public Product ProductNav { get; set; }

        public int ProductQuantity { get; set; }
    }
}
