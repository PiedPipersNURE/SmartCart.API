using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCart.DataProvider.Models
{
    public class CartMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartMemberID { get; set; }
        public Guid CartID { get; set; }
        public Guid MemberID { get; set; }
    }
}
