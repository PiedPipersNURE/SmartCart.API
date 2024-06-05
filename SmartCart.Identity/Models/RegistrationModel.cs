namespace SmartCart.Identity.Models
{
    public class RegistrationModel
    {
        public string? GoogleID { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; } = new DateTime(2000, 1, 1);
        public string Password { get; set; }
    }
}
