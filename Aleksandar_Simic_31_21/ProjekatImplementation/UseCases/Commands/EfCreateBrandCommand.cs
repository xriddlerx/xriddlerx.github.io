using FluentValidation;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.Brands;
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
    public class EfCreateBrandCommand : EfUseCase, ICreateBrandCommand
    {
        private CreateBrandValidator _validator;
        public EfCreateBrandCommand(ProjekatContext context, CreateBrandValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Create New Brand using EF";

        public void Execute(BrandCreateDTO data)
        {
            _validator.ValidateAndThrow(data);

            Brand brandToAdd = new Brand
            {
                Name = data.Name
            };

            Context.Brands.Add(brandToAdd);
            Context.SaveChanges();
        }
    }
}
