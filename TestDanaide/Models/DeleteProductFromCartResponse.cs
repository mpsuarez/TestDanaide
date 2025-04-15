using TestDanaide.Persistence.Entities;

namespace TestDanaide.Models
{
    public class DeleteProductFromCartResponse
    {

        public Guid Id { get; set; }

        public IList<DeleteProductFromCartResponseProduct> Products { get; set; }

        public decimal Total { get; set; }

    }

    public class DeleteProductFromCartResponseProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
