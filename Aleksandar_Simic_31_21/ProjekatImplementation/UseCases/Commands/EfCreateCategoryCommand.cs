using FluentValidation;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.Category;
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
    public class EfCreateCategoryCommand : EfUseCase ,ICreateCategoryCommand
    {
        private CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(ProjekatContext context, CreateCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Create Category using EF";

        public void Execute(CategoryCreate data)
        {
            _validator.ValidateAndThrow(data);

            Category category = new Category
            {
                Name = data.Name
            };

            Context.Categories.Add(category);
            Context.SaveChanges();
        }
    }
}
