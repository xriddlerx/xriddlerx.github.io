using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrandCategoryId { get; set; }
        public int GalleryId { get; set; }
        public Gallery Gallery { get; set; }
        public BrandCategory BrandCategory { get; set; }
        public ICollection<Price> Prices { get; set; } = new HashSet<Price>();
        public ICollection<ProductCart> ProductCarts { get; set; } = new HashSet<ProductCart>();
        public ICollection<ProductOrder> ProductOrders { get; set; } = new HashSet<ProductOrder>();
    }
}
