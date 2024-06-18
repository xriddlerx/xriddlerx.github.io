using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.Users;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public UserController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }

        // GET: api/<UserController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }


        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserCreateDTO dto, [FromServices] ICreateUserCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Created();
        }
    }
}
