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
    public class CreateCategoryValidator : AbstractValidator<CategoryCreate>
    {
        private ProjekatContext _context;
        public CreateCategoryValidator(ProjekatContext context) 
        {
            _context = context;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name required")
                                            .MinimumLength(3)
                                            .WithMessage("Category name must have minimum 3 characters")
                                            .MaximumLength(20)
                                            .WithMessage("Brand name can have maximum 20 characters")
                                            .Must(x => !_context.Categories.Any(y => y.Name == x))
                                            .WithMessage("Name already exists in database");
        }
    }
}
