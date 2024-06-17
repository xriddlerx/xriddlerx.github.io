using Microsoft.IdentityModel.Tokens;
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
    public class EfGetProductQuery : EfUseCase, IGetProductQuery
    {
        public EfGetProductQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Search products using EF";

        public PagedResponse<GetProductDTO> Exectue(ProductSearch search)
        {
            var query = Context.Products.AsQueryable();
            

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            if(search.BrandCategory.HasValue)
            {
                query = query.Where(x => x.BrandCategoryId ==  search.BrandCategory.Value);
            }

            if (search.MinPrice.HasValue)
            {
                query = query.Where(x => x.Prices.OrderByDescending(x => x.DateOfPrice).Select(x => x.ProductPrice).FirstOrDefault() >= search.MinPrice.Value);    
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Prices.OrderByDescending(x => x.DateOfPrice).Select(x => x.ProductPrice).FirstOrDefault() <= search.MaxPrice.Value);
            }

            if(search.OrderByName.HasValue)
            {
                if(search.OrderByName == 0) 
                {
                    query = query.OrderBy(x => x.Name);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Name);
                }
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetProductDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description == null ? "No description" : x.Description,
                    BrandCategory = x.BrandCategory.Category.Name + " - " + x.BrandCategory.Brand.Name,
                    Price = x.Prices.OrderByDescending(x => x.DateOfPrice).Select(x => x.ProductPrice).FirstOrDefault(),
                    Image = x.Gallery.PathName
                }).ToList()
            };

            return response;
        }
    }
}
