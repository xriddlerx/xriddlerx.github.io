using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.Brands;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IApplicationActor actor;
        public readonly UseCaseHandler handler;

        public BrandController(IApplicationActor actor, UseCaseHandler handler)
        {
            this.actor = actor;
            this.handler = handler;
        }


        // GET: api/<BrandController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] BrandSearch dto,[FromServices] IGetBrandsQuery getBrandsQuery)
        {
            return Ok(handler.HandleQuery(getBrandsQuery, dto));
        }

        // POST api/<BrandController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] BrandCreateDTO dto, [FromServices] ICreateBrandCommand command)
        {
              handler.HandleCommand(command, dto);
              return Created();
        }

        // DELETE api/<BrandController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBrandCommand command)
        {
            handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
