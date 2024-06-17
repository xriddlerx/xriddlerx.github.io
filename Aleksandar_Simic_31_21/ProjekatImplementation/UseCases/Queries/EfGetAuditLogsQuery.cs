using Newtonsoft.Json;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Queries;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Queries
{
    public class EfGetAuditLogsQuery : EfUseCase, IGetAuditLogsQuery
    {
        public EfGetAuditLogsQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 23;

        public string Name => "Search AuditLogs usign EF";

        public PagedResponse<GetAuditLogs> Exectue(AuditLogSearch search)
        {
            var query = Context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.UseCaseName) || !string.IsNullOrWhiteSpace(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.FirstName) || !string.IsNullOrWhiteSpace(search.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.LastName) || !string.IsNullOrWhiteSpace(search.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.ExecutedAt >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.ExecutedAt <= search.DateTo.Value);
            }

            if (search.OrderByDate.HasValue)
            {
                if (search.OrderByDate == 0)
                {
                    query = query.OrderBy(x => x.ExecutedAt);
                }
                else
                {
                    query = query.OrderByDescending(x => x.ExecutedAt);
                }
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetAuditLogs>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetAuditLogs
                {
                    AuditLogId = x.Id,
                    UseCaseName = x.UseCaseName,
                    FullName = x.FirstName + " " + x.LastName,
                    ExecutedAt = x.ExecutedAt,
                    UseCaseData = x.UseCaseData,
                }).ToList()
            };

            return response;
        }
    }
}
