using FluentValidation;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.Validators
{
    public class UpdateQuantityValidator : AbstractValidator<UpdateQuantityDTO>
    {
        private readonly ProjekatContext _context;
        private readonly IApplicationActor _actor;
        public UpdateQuantityValidator(ProjekatContext context, IApplicationActor actor) 
        {
            _context = context;
            _actor = actor;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.ProductId).NotEmpty()
                                                   .WithMessage("Product field can't be empty")
                                                   .Must(x => _context.ProductCart.Any(y => y.ProductId == x && y.Cart.UserId == actor.Id))
                                                   .WithMessage("This product doesn't exist in your cart");

            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity field can't be empty")
                                                   .Must(x => x > 0).WithMessage("Quantity has to be minimum 1");
        }
    }
}
