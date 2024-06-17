using ProjekatApplication.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO.Searches
{
    public class ProductSearch : PagedSearch
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? BrandCategory { get; set; }
        public OrderBy? OrderByName { get; set; }
    }

    public enum OrderBy
    {
        asc,
        desc
    }
}
