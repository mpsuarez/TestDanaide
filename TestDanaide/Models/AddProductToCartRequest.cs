using System.ComponentModel.DataAnnotations;

namespace TestDanaide.Models
{
    public class AddProductToCartRequest
    {
        
        [Required]
        public Guid CartId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

    }
}
