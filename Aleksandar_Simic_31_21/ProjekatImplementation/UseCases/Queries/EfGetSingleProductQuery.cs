using Microsoft.EntityFrameworkCore;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Queries;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Queries
{
    public class EfGetSingleProductQuery : EfUseCase,IGetSingleProductQuery
    {
        public EfGetSingleProductQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Get Single product using EF";

        public GetProductDTO Exectue(int search)
        {
            var product = Context.Products.Include(x=>x.BrandCategory)
                                                  .Include(x=>x.Gallery)
                                                  .Include(x=>x.Prices)
                                                  .Include(x=>x.BrandCategory.Brand)
                                                  .Include(x=>x.BrandCategory.Category)
                                                  .FirstOrDefault(x => x.Id == search);

            if(product == null)
            {
                throw new KeyNotFoundException();
            }

            GetProductDTO dto = new GetProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description == null ? "No description" : product.Description,
                BrandCategory = product.BrandCategory.Brand.Name + " - " + product.BrandCategory.Category.Name,
                Price = product.Prices.OrderByDescending(x => x.DateOfPrice).Select(x => x.ProductPrice).FirstOrDefault(),
                Image = product.Gallery.PathName
            };

            return dto;
        }
    }
}
