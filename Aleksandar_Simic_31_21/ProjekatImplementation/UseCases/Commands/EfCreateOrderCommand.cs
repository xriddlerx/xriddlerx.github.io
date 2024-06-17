using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjekatApplication;
using ProjekatApplication.UseCases.Commands.Orders;
using ProjekatDataAccess;
using ProjekatDomain;
using ProjekatImplementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfCreateOrderCommand : EfUseCase,ICreateOrderCommand
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;
        public EfCreateOrderCommand(ProjekatContext context, IApplicationActor actor, IUseCaseLogger logger) : base(context)
        {
            _actor = actor;
            _logger = logger;
        }
        public int Id => 20;

        public string Name => "Place Order using EF";
        public void Execute() 
        {
            var userCart = Context.Carts.Include(x => x.ProductCarts).FirstOrDefault(x => x.UserId == _actor.Id);

            if(userCart.ProductCarts.Count <= 0) 
            {
                throw new KeyNotFoundException();
            }

            List<ProductOrder> productOrders = new List<ProductOrder>();

            foreach(ProductCart element in  userCart.ProductCarts) 
            {
                ProductOrder product = new ProductOrder();
                product.ProductId = element.ProductId;
                product.Quantity = element.Quantity;
                productOrders.Add(product);
            }

            foreach(ProductCart element in userCart.ProductCarts)
            {
                Context.ProductCart.Remove(element);
                Context.SaveChanges();
            }

            Order order = new Order
            {
                UserId = _actor.Id,
                OrderDate = DateTime.UtcNow,
                ProductOrders = productOrders
            };

            UseCaseLog log = new UseCaseLog
            {
                UseCaseName = Name,
                UseCaseData = JsonConvert.SerializeObject(order),
                FirstName = _actor.FirstName,
                LastName = _actor.LastName,
                ExecutedAt = DateTime.UtcNow
            };
            Context.UseCaseLogs.Add(log);
            Context.Orders.Add(order);
            Context.SaveChanges();
        }
    }
}
