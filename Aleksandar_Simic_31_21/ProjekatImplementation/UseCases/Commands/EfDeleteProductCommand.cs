using ProjekatApplication;
using ProjekatApplication.UseCases.Commands.Products;
using ProjekatDataAccess;
using ProjekatDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfDeleteProductCommand : EfUseCase,IDeleteProductCommand
    {
        private IApplicationActor _actor;
        public EfDeleteProductCommand(ProjekatContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 22;

        public string Name => "Remove product using EF";

        public void Execute(int data)
        {
            var product = Context.Products.Where(x => x.Id == data).First();

            if(product == null) 
            {
                throw new KeyNotFoundException();
            }

            if(Context.ProductOrder.Any(x => x.ProductId == product.Id))
            {
                throw new InvalidOperationException();
            }

            var carts = Context.ProductCart.Where(x => x.ProductId ==  product.Id).ToList();
            var prices = Context.Prices.Where(x => x.ProductId == product.Id).ToList();
            foreach( Price price in prices)
            {
                Context.Prices.Remove(price);
                Context.SaveChanges();
            }

            foreach(ProductCart cart in carts)
            {
                Context.ProductCart.Remove(cart);
            }
            Context.Products.Remove(product);
            Context.SaveChanges();
        }
    }
}
