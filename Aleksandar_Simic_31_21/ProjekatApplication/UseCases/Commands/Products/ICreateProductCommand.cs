﻿using ProjekatApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatApplication.UseCases.Commands.Products
{
    public interface ICreateProductCommand : ICommand<ProductCreateDTO>
    {
    }
}
