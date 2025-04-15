using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities.Base;

namespace TestDanaide.Persistence.Entities
{
    public class User : Entity, IEntity
    {
        [Required]
        public string Dni { get; set; }
        public bool IsVip { get; set; } = false;
        public IList<Cart> Carts { get; set; }
    }
}
