using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.BrandCat;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandCategoryController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public BrandCategoryController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }

        // GET: api/<BrandCategoryController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] BCSearch dto, [FromServices] IGetBCQuery query)
        {
            return Ok(_handler.HandleQuery(query,dto));
        }

        // POST api/<BrandCategoryController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] BCCreateDTO dto, [FromServices] ICreateBCCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Created();
        }

        // DELETE api/<BrandCategoryController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBCCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
