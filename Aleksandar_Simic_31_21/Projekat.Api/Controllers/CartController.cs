using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.Cart;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public CartController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }

        // GET: api/<CartController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] CartSearch search, [FromServices] IGetCartInfoQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // POST api/<CartController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CartCreateForUserDTO dto, [FromServices] ICreateProductCartCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Created();
        }
    }
}
