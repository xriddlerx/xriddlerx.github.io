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
    public class EfGetBrandsQuery : EfUseCase ,IGetBrandsQuery
    {
        public EfGetBrandsQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 2;

        public string Name => "Brand search";


        public PagedResponse<GetBrandDTO> Exectue(BrandSearch search)
        {
            var query = Context.Brands.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            if (search.OrderByName.HasValue)
            {
                if (search.OrderByName == 0)
                {
                    query = query.OrderBy(x => x.Name);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Name);
                }
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetBrandDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetBrandDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return response;
        }
    }
}
