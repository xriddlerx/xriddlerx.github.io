using ProjekatApplication.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO.Searches
{
    public class CategorySearch : PagedSearch
    {
        public string Name { get; set; }
        public OrderByCategoryName? OrderByName { get; set; }
    }
    public enum OrderByCategoryName
    {
        asc,
        desc
    }
}
