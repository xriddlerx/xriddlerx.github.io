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
    public class CreateProductValidator : AbstractValidator<ProductCreateDTO>
    {
        private ProjekatContext _context;

        public CreateProductValidator(ProjekatContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotEmpty()
                                             .WithMessage("Name field can't be empty")
                                             .MinimumLength(3)
                                             .WithMessage("Minimum length for the name is 3 characters")
                                             .MaximumLength(30)
                                             .WithMessage("Maximum length for the name is 30 characters")
                                             .Must(x => !_context.Products.Any(y => y.Name == x))
                                             .WithMessage("This name already exists");

            RuleFor(x => x.Price).NotEmpty()
                                              .WithMessage("Price field can't be empty")
                                              .Must(x => x > 0)
                                              .WithMessage("Price must be above 0");

            RuleFor(x => x.BrandCategoryId).NotEmpty()
                                                        .WithMessage("Brand Category filed can't be empty")
                                                        .Must(x => _context.BrandCategory.Any(y => y.Id == x))
                                                        .WithMessage("Brand Category doesn't exist in database");
        }
    }
}
