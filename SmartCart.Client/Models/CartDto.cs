namespace SmartCart.Client.Models
{
    public class CartDto
    {
        public Guid CartID { get; set; }
        public string CartName { get; set; }
        public Guid CartOwnerID { get; set; }
    }
}
