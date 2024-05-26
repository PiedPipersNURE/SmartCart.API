using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCart.Identity.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID { get; set; }
        public string? GoogleID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string Username { get; set; }
        public bool IsNotificationEnabled { get; set; }
    }
}
