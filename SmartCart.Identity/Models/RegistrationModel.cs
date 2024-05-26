namespace SmartCart.Identity.Models
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }
    }
}
