using Microsoft.EntityFrameworkCore;
using ProjekatApplication.UseCases.Commands.Category;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfDeleteCategoryCommand : EfUseCase,IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(ProjekatContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "Delete Category using EF";

        public void Execute(int data)
        {
            var category = Context.Categories.Include(c => c.BrandCategories).FirstOrDefault(x => x.Id == data);

            if (category == null)
            {
                throw new KeyNotFoundException();
            }

            if (category.BrandCategories.Count > 0)
            {
                throw new InvalidOperationException();
            }

            Context.Categories.Remove(category);
            Context.SaveChanges();
        }
    }
}
