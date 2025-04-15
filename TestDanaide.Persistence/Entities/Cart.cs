using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities.Base;
using TestDanaide.Persistence.Enums;

namespace TestDanaide.Persistence.Entities
{
    public class Cart : Entity, IEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public CartType Type { get; set; }
        public IList<CartProduct> CartProducts { get; set; }
        public decimal Total { get; set; }
    }
}
