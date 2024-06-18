using Microsoft.EntityFrameworkCore;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Queries;
using ProjekatDataAccess;
using ProjekatDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjekatImplementation.UseCases.Queries
{
    public class EfGetCartInfoQuery : EfUseCase,IGetCartInfoQuery
    {
        private readonly IApplicationActor _actor;
        public EfGetCartInfoQuery(ProjekatContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 17;

        public string Name => "Searching items in cart using EF";

        public PagedResponse<ProductsFromCartInfo> Exectue(CartSearch search)
        {
            var query = Context.ProductCart.Where(x => x.Cart.User.Id == _actor.Id)
                                    .Include(y => y.Product)
                                    .ThenInclude(c => c.Prices)
                                    .ThenInclude(g => g.Product.Gallery)
                                    .ThenInclude(bg => bg.Product.BrandCategory)
                                    .ThenInclude(ca => ca.Category)
                                    .ThenInclude(b => b.BrandCategories)
                                    .ThenInclude(br => br.Brand)
                                    .AsQueryable();

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<ProductsFromCartInfo>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ProductsFromCartInfo
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    ProductPrice = x.Product.Prices.OrderByDescending(c => c.DateOfPrice).Select(y => y.ProductPrice).FirstOrDefault(),
                    Quantity = x.Quantity,
                    BrandCategory = x.Product.BrandCategory.Brand.Name +" - "+ x.Product.BrandCategory.Category.Name,
                    ProductDescription = x.Product.Description == null ? "No discription" : x.Product.Description,
                    ImgPath = x.Product.Gallery.PathName
                    
                }).ToList()
            };

            return response;
        }
    }
}
