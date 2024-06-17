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
    public class CreateBrandValidator : AbstractValidator<BrandCreateDTO>
    {
        private ProjekatContext _context;
        public CreateBrandValidator(ProjekatContext context) 
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotEmpty()
                                            .WithMessage("Brand name required")
                                            .MinimumLength(3)
                                            .WithMessage("Brand name must have minimum 3 characters")
                                            .MaximumLength(20)
                                            .WithMessage("Brand name can have maximum 20 characters")
                                            .Must(x => !_context.Brands.Any(y => y.Name == x))
                                            .WithMessage("Name already exists in database");
        }
    }
}
