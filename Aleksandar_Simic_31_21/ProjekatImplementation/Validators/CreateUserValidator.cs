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
    public class CreateUserValidator : AbstractValidator<UserCreateDTO>
    {
        private readonly ProjekatContext _context;
        public CreateUserValidator(ProjekatContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FirstName).NotEmpty()
                                                .WithMessage("First name can't be empty")
                                                .MinimumLength(3)
                                                .WithMessage("First name has to have minimum 3 characters")
                                                .MaximumLength(30)
                                                .WithMessage("First name can have up to 30 characters");

            RuleFor(x => x.LastName).NotEmpty()
                                                .WithMessage("Last name can't be empty")
                                                .MinimumLength(3)
                                                .WithMessage("Last name has to have minimum 3 characters")
                                                .MaximumLength(30)
                                                .WithMessage("Last name can have up to 30 characters");

            RuleFor(x => x.Email).NotEmpty()
                                                .WithMessage("Email can't be empty")
                                                .MaximumLength(35)
                                                .WithMessage("Email can have up to 35 characters")
                                                .Must(x => x.Contains("@gmail.com") || x.Contains("@ict.edu.rs") || x.Contains("@yahoo.com"))
                                                .WithMessage("Email must contain @ and either: gmail.com/ict.edu.rs/yahoo.com")
                                                .Must(x => !_context.Users.Any(y => y.Email == x))
                                                .WithMessage("Already exists");

            RuleFor(x => x.Password).NotEmpty()
                                               .WithMessage("Password field can't be empty")
                                               .MinimumLength(8)
                                               .WithMessage("Password has to be minimum 8 characters")
                                               .MaximumLength(15)
                                               .WithMessage("Password can be up to 15 characters");
        }
    }
}
