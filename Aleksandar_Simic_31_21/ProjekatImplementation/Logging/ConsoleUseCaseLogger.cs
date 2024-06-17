using Newtonsoft.Json;
using ProjekatApplication;
using ProjekatApplication.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.Logging
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            Console.WriteLine($"{DateTime.UtcNow}: {actor.FirstName} {actor.LastName} is" +
                $" trying to execute {useCase.Name} using data {JsonConvert.SerializeObject(useCaseData)}");
        }
    }
}
