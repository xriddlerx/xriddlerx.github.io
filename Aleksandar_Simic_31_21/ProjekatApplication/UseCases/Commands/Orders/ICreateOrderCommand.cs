using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.UseCases.Commands.Orders
{
    public interface ICreateOrderCommand : IUseCase
    {
        void Execute();
    }
}
