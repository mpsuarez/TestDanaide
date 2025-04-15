using TestDanaide.Persistence.Entities;

namespace TestDanaide.Models
{
    public class AddProductToCartResponse
    {

        public Guid Id { get; set; }

        public IList<Product> Products { get; set; }

        public decimal Total { get; set; }

    }
}
