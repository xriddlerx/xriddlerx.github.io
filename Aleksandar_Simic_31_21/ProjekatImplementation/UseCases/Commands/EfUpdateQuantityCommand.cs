using FluentValidation;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.ProductCart;
using ProjekatDataAccess;
using ProjekatImplementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfUpdateQuantityCommand : EfUseCase,IUpdateQuantityCommand
    {
        private readonly IApplicationActor _actor;
        private readonly UpdateQuantityValidator _validator;
        public EfUpdateQuantityCommand(ProjekatContext context, IApplicationActor actor, UpdateQuantityValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "Update quantity for product in cart using EF";

        public void Execute(UpdateQuantityDTO data)
        {
            _validator.ValidateAndThrow(data);
            var productInCart = Context.ProductCart.Where(x => x.ProductId == data.ProductId && x.Cart.UserId == _actor.Id).First();

            productInCart.Quantity = data.Quantity;
            Context.SaveChanges();
        }
    }
}
