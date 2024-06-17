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

namespace ProjekatImplementation.UseCases.Queries
{
    public class EfGetOrderQuery : EfUseCase,IGetOrderQuery
    {
        private readonly IApplicationActor _actor;
        public EfGetOrderQuery(ProjekatContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 21;

        public string Name => "Search orders using EF";

        public PagedResponse<GetOrdersDTO> Exectue(OrderSearch search)
        {
            var query = Context.Orders.Where(x => x.UserId == _actor.Id)
                                                    .Include(x => x.ProductOrders)
                                                    .ThenInclude(x => x.Product)
                                                    .ThenInclude(x => x.Prices)
                                                    .AsQueryable();

            if (search.OrderByDate.HasValue)
            {
                if(search.OrderByDate == 0)
                {
                    query = query.OrderBy(x => x.OrderDate);
                }
                else
                {
                    query = query.OrderByDescending(x => x.OrderDate);
                }
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetOrdersDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetOrdersDTO
                {
                    Orders = new List<ODTO>
                    {
                        new ODTO
                        {
                            OrderId = x.Id,
                            OrderDate = x.OrderDate,
                            Products = x.ProductOrders.Select(p => new PDTO
                            {
                                ProductId = p.ProductId,
                                ProductName = p.Product.Name,
                                Price = p.Product.Prices.OrderByDescending(c => c.DateOfPrice).Select(y => y.ProductPrice).First(),
                                Quantity = p.Quantity,
                                TotalPrice = p.Quantity * p.Product.Prices.OrderByDescending(c => c.DateOfPrice).Select(y => y.ProductPrice).First()
                            }),
                        }
                    }
                }).ToList()
            };

            return response;
        }
    }
}
