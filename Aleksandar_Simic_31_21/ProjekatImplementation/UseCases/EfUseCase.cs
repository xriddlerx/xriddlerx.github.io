using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly ProjekatContext _context;

        protected EfUseCase(ProjekatContext context)
        {
            _context = context;
        }

        protected ProjekatContext Context => _context;
    }
}
