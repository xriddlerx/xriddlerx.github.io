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
    public class EfGetCategoryQuery : EfUseCase, IGetCategoriesQuery
    {
        public EfGetCategoryQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Category search";

        public PagedResponse<GetCategoryDTO> Exectue(CategorySearch search)
        {
            var query = Context.Categories.AsQueryable();

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

            var response = new PagedResponse<GetCategoryDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetCategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return response;
        }
    }
}
