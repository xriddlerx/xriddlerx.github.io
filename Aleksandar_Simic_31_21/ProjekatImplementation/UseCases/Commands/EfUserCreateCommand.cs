using FluentValidation;
using ProjekatApplication.DTO;
using ProjekatApplication.Email;
using ProjekatApplication.UseCases.Commands.Users;
using ProjekatDataAccess;
using ProjekatDomain;
using ProjekatImplementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfUserCreateCommand : EfUseCase, ICreateUserCommand
    {
        private readonly CreateUserValidator _validator;
        private readonly IEmailSender _sender;
        public EfUserCreateCommand(ProjekatContext context, CreateUserValidator validator, IEmailSender sender) : base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public int Id => 14;

        public string Name => "Resgiser User using EF";

        public void Execute(UserCreateDTO data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                UseCases = new List<UserUseCase>()
                {
                    new UserUseCase { UseCaseId = 8 },
                    new UserUseCase { UseCaseId = 10 },
                    new UserUseCase { UseCaseId = 11 },
                    new UserUseCase { UseCaseId = 16 },
                    new UserUseCase { UseCaseId = 17 },
                    new UserUseCase { UseCaseId = 18 },
                    new UserUseCase { UseCaseId = 19 },
                    new UserUseCase { UseCaseId = 20 },
                    new UserUseCase { UseCaseId = 21 }
                }
            };

            Context.Users.Add(user);
            Context.SaveChanges();

            _sender.Send(new SendEmailDto
            {
                Content = "<h1>You have successfully registered</h1>",
                SendTo = data.Email,
                Subject = "Registration"
            });
        }
    }
}
