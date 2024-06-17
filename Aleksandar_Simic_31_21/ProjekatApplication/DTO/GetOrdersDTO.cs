using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO
{
    public class GetOrdersDTO
    {
        public IEnumerable<ODTO> Orders { get; set; }
    }

    public class ODTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<PDTO> Products { get; set; }
    }
    public class PDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
