using FluentValidation;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.BrandCat;
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
    public class EfCreateBCCommand : EfUseCase,ICreateBCCommand
    {
        private CreateBCValidator _validator;
        public EfCreateBCCommand(ProjekatContext context, CreateBCValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Create Brand-Category using EF";

        public void Execute(BCCreateDTO data)
        {
            _validator.ValidateAndThrow(data);

            BrandCategory bc = new BrandCategory
            {
                BrandId = data.BrandId,
                CategoryId = data.CategoryId
            };

            Context.BrandCategory.Add(bc);
            Context.SaveChanges();
        }
    }
}
