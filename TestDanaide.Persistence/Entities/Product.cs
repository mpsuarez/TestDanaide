using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities.Base;

namespace TestDanaide.Persistence.Entities
{
    public class Product : Entity, IEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IList<CartProduct> CartProducts { get; set; }
    }
}
