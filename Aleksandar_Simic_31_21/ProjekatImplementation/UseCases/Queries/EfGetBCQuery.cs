using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Queries;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjekatImplementation.UseCases.Queries
{
    public class EfGetBCQuery : EfUseCase,IGetBCQuery
    {
        public EfGetBCQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Get Brand-Category";

        public PagedResponse<GetBCDTO> Exectue(BCSearch search)
        {
            var query = Context.BrandCategory.AsQueryable();

            if (search.BrandId.HasValue)
            {
                query = query.Where(x => x.BrandId == search.BrandId);
            }

            if (search.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == search.CategoryId);
            }

            

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetBCDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetBCDTO
                {
                    Id = x.Id,
                    BrandName = x.Brand.Name,
                    CategoryName = x.Category.Name
                    
                }).ToList()
            };
            return response;
        }
    }
}
