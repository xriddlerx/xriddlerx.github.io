using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.Category;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public CategoryController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }

        // GET: api/<CategoryController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch dto, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        // POST api/<CategoryController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryCreate data, [FromServices] ICreateCategoryCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

        // DELETE api/<CategoryController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
