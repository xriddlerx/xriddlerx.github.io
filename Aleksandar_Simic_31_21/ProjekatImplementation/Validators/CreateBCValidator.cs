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
    public class CreateBCValidator : AbstractValidator<BCCreateDTO>
    {
        private ProjekatContext _context;
        public CreateBCValidator(ProjekatContext context) 
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.BrandId).NotEmpty()
                                            .WithMessage("Brand required")
                                            .Must(x => _context.Brands.Any(y => y.Id == x))
                                            .WithMessage("Brand doesn't exist in the database")
                                            .Must((b,x) => !_context.BrandCategory.Any(y => y.BrandId == x && y.CategoryId == b.CategoryId))
                                            .WithMessage("You have already added this combination");
            RuleFor(x => x.CategoryId).NotEmpty()
                                            .WithMessage("Category required")
                                            .Must(x => _context.Categories.Any(y => y.Id == x))
                                            .WithMessage("Category doesn't exist in the database")
                                            .Must((b, x) => !_context.BrandCategory.Any(y => y.CategoryId == x && y.BrandId == b.BrandId))
                                            .WithMessage("You have already added this combination");

        }
    }
}
