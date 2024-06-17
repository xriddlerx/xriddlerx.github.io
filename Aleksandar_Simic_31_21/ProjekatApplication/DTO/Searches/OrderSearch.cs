using ProjekatApplication.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO.Searches
{
    public enum OrderByDate
    {
        asc, 
        desc
    }
    public class OrderSearch : PagedSearch
    {
        public OrderByDate? OrderByDate { get; set; } //0 je asc 1 je desc
    }
}
