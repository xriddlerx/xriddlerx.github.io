using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class Cart : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ProductCart> ProductCarts { get; set; } = new HashSet<ProductCart>();
    }
}
