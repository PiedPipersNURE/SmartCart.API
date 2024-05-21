using SmartCart.DataProvider.Enums;

namespace SmartCart.DataProvider.Models
{
    public class ProductDto
    {
        public Guid ProductID { get; set; }
        public Guid CartID { get; set; }
        public string ProductName { get; set; }
        public Guid? BuyerID { get; set; }
        public int ProductQuantity { get; set; } = 1;
        public Quantity QuantityType { get; set; }
        public DateTime? EndTime { get; set; } = DateTime.Now.AddHours(6);
        public bool IsBought { get; set; } = false;
    }
}
