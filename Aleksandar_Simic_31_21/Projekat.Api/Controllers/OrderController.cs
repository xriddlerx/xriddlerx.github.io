using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.Orders;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public OrderController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }

        // GET: api/<OrderController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearch search,[FromServices] IGetOrderQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }

        // POST api/<OrderController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromServices] ICreateOrderCommand command)
        {
            command.Execute();
            return Created();
        }
    }
}
