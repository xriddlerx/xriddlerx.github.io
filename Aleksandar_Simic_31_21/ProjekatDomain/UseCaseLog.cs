using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatDomain
{
    public class UseCaseLog
    {
        public int Id { get; set; }
        public string UseCaseName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UseCaseData { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}
