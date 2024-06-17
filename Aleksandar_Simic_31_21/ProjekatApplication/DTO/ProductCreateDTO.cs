using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO
{
    public class ProductCreateDTO
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BrandCategoryId { get; set; }
        public IFormFile Image { get; set;}
    }
}
