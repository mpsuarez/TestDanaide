using TestDanaide.Persistence.Entities;

namespace TestDanaide.Models
{
    public class AddProductToCartResponse
    {

        public Guid Id { get; set; }

        public IList<AddProductToCartResponseProduct> Products { get; set; }

        public decimal Total { get; set; }

    }

    public class AddProductToCartResponseProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
