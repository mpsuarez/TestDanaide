using TestDanaide.Persistence.Entities;

namespace TestDanaide.Models
{
    public class DeleteProductFromCartResponse
    {

        public Guid Id { get; set; }

        public IList<Product> Products { get; set; }

        public decimal Total { get; set; }

    }
}
