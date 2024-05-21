using SmartCart.Client.Enums;

namespace SmartCart.Client.Models.Product
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string? BuyerID { get; set; }
        public int ProductQuantity { get; set; }
        public Quantity QuantityType { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsBought { get; set; }
    }
}
