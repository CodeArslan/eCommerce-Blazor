using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Data
{
    public class OrderHeader
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name="Grand Total")]
        public double GrandTotal { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

    }
}
