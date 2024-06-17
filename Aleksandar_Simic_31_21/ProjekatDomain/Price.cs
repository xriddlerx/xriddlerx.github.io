using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class Price : Entity
    {
        public decimal ProductPrice {  get; set; }
        public DateTime DateOfPrice { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
