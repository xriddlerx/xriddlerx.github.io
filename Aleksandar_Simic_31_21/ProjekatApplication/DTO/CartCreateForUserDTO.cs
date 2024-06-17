using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO
{
    public class CartCreateForUserDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }
    }

    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
