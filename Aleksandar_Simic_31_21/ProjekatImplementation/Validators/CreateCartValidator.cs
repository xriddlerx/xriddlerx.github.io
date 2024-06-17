using FluentValidation;
using ProjekatApplication.DTO;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.Validators
{
    public class CreateCartValidator : AbstractValidator<CartCreateForUserDTO>
    {
        private readonly ProjekatContext _context;
        public CreateCartValidator(ProjekatContext context) 
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleForEach(x => x.Products).NotEmpty().WithMessage("Fileds must be field").Must(y => _context.Products.Any(x => x.Id == y.ProductId))
                                                        .WithMessage("Product that you wanted to add doesn't exist in database")
                                                        .Must(y => y.Quantity > 0)
                                                        .WithMessage("Quantity must be at least 1");
        }
    }
}
