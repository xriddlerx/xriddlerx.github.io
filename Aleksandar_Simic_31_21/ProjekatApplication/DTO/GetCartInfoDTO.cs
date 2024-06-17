using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO
{
    public class ProductsFromCartInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductDescription { get; set; }
        public string BrandCategory { get; set; }
        public string ImgPath { get; set;}
    }
}
