﻿using ProjekatApplication;
using ProjekatApplication.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(IUseCase useCase, IApplicationActor actor) 
            : base($"Actor with an id of {actor.Id} - {actor.FirstName} {actor.LastName} tried to execute {useCase.Name}") 
        {
            
        }
    }
}
