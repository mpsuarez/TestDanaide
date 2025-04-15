using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities.Base;

namespace TestDanaide.Persistence.Entities
{
    public class CartProduct : Entity, IEntity
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
