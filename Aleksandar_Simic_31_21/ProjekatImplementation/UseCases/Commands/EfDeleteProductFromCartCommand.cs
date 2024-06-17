using ProjekatApplication;
using ProjekatApplication.UseCases.Commands.ProductCart;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfDeleteProductFromCartCommand : EfUseCase,IDeleteProductFromCartCommand
    {
        private readonly IApplicationActor _actor;

        public EfDeleteProductFromCartCommand(ProjekatContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 19;

        public string Name => "Delete product from cart using EF";

        public void Execute(int data)
        {
            var product = Context.ProductCart.Where(x => x.ProductId == data && x.Cart.UserId == _actor.Id).FirstOrDefault();
            
            if(product == null)
            {
                throw new KeyNotFoundException();
            }

            Context.ProductCart.Remove(product);
            Context.SaveChanges();
        }
    }
}
