using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.UseCases.Queries
{
    public interface IGetBCQuery : IQuery<PagedResponse<GetBCDTO>, BCSearch>
    {
    }
}
