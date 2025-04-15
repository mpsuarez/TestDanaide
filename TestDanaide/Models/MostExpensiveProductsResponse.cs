using TestDanaide.Persistence.Entities;

namespace TestDanaide.Models
{
    public class MostExpensiveProductsResponse
    {

        public IList<MostExpensiveProductsResponseProduct> Products { get; set; }

    }

    public class MostExpensiveProductsResponseProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
