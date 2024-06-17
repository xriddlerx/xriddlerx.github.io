using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } = new HashSet<ProductOrder>();

    }
}
