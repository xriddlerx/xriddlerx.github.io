using ProjekatApplication.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO.Searches
{
    public class AuditLogSearch : PagedSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UseCaseName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public OrderByDate? OrderByDate { get; set; }
    }
}
