using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.ProductCart;
using ProjekatImplementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCartController : ControllerBase
    {
        public readonly IApplicationActor _actor;
        public readonly UseCaseHandler _handler;

        public ProductCartController(IApplicationActor actor, UseCaseHandler handler)
        {
            _actor = actor;
            _handler = handler;
        }

        // PUT api/<ProductCartController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateQuantityDTO dto, [FromServices] IUpdateQuantityCommand command)
        {
            dto.ProductId = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<ProductCartController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProductFromCartCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
