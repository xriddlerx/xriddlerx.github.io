using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Commands.Products;
using ProjekatApplication.UseCases.Queries;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public ProductController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearch search, [FromServices] IGetProductQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSingleProductQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }

        // POST api/<ProductController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromForm] ProductCreateDTO dto, [FromServices] ICreateProductCommand command)
        {
            _handler.HandleCommand(command,dto);
            return Created();
        }

        // PUT api/<ProductController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UpdateProductDTO dto, [FromServices] IUpdateProductCommand command)
        {
            dto.ProductId = id;
            _handler.HandleCommand(command,dto);
            return NoContent();
        }

        // DELETE api/<ProductController>/5
        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
