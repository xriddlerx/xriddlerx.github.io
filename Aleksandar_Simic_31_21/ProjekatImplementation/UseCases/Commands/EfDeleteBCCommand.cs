using ProjekatApplication.UseCases.Commands.BrandCat;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfDeleteBCCommand : EfUseCase,IDeleteBCCommand
    {
        public EfDeleteBCCommand(ProjekatContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Remove specific Brand-Category using EF";

        public void Execute(int data)
        {
            var bc = Context.BrandCategory.FirstOrDefault(x => x.Id == data);
            if (bc == null)
            {
                throw new KeyNotFoundException();
            }

            if(Context.Products.Any(p => p.BrandCategoryId == data))
            {
                throw new InvalidOperationException();
            }

            Context.BrandCategory.Remove(bc);
            Context.SaveChanges();
        }
    }
}
