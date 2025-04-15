using System.ComponentModel.DataAnnotations;

namespace TestDanaide.Models
{
    public class DeleteProductFromCartRequest
    {

        [Required]
        public Guid CartId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

    }
}
