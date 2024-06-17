using Microsoft.EntityFrameworkCore;
using ProjekatApplication.UseCases.Commands.Brands;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfDeleteBrandCommand : EfUseCase ,IDeleteBrandCommand
    {
        public EfDeleteBrandCommand(ProjekatContext context) : base(context)
        {
        }

        public int Id => 3;

        public string Name => "Remove specific brand using Ef";

        public void Execute(int data)
        {
            var brand = Context.Brands.Include(b => b.BrandCategories).FirstOrDefault(x => x.Id == data);

            if(brand == null)
            {
                throw new KeyNotFoundException();
            }

            if (brand.BrandCategories.Count > 0)
            {
                throw new InvalidOperationException();
            }

            Context.Brands.Remove(brand);
            Context.SaveChanges();
        }
    }
}
