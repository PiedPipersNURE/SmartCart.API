using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartCart.DataProvider.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartID { get; set; }
        public string CartName { get; set; }
        public Guid CartOwnerID { get; set; }
    }
}
