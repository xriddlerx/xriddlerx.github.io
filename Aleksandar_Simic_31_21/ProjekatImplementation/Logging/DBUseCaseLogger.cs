using Newtonsoft.Json;
using ProjekatApplication;
using ProjekatApplication.UseCases;
using ProjekatDataAccess;
using ProjekatDomain;
using ProjekatImplementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.Logging
{
    public class DBUseCaseLogger : EfUseCase,IUseCaseLogger
    {
        public DBUseCaseLogger(ProjekatContext context) : base(context)
        {
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            UseCaseLog log = new UseCaseLog
            {
                UseCaseName = useCase.Name,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                UseCaseData = JsonConvert.SerializeObject(useCaseData),
                ExecutedAt = DateTime.UtcNow,
            };

            Context.UseCaseLogs.Add(log);
            Context.SaveChanges();
        }
    }
}
