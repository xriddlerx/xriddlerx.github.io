using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class BrandCategory : Entity
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
