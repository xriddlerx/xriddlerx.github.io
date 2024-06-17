using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.DTO
{
    public class GetAuditLogs
    {
        public int AuditLogId { get; set; }
        public string UseCaseName { get; set; }
        public string FullName { get; set; }
        public DateTime ExecutedAt { get; set; }
        public object UseCaseData { get; set; }
    }
}
