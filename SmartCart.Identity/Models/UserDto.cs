namespace SmartCart.Identity.Models
{
    public class UserDto
    {
        public Guid UserID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public bool IsNotificationEnabled { get; set; }
    }
}
