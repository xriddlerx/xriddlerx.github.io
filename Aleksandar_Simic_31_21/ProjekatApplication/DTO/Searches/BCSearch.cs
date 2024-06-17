using ProjekatApplication.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO.Searches
{
    public class BCSearch : PagedSearch
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
    }
}
