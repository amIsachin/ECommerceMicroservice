using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.Quesries;

namespace Product.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AddProductCommandHandler _commandHandler;
        private readonly GetAllProductQueryHandler _queryHandler;

        public ProductController(AddProductCommandHandler commandHandler, GetAllProductQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost("CreateProduct", Name = "Create")]
        public async Task<IActionResult> CreateProduct(AddProductCommand command)
        {
            var id = await _commandHandler.Handle(command);

            return Ok(id);

            // return CreatedAtAction(nameof(GetAll), new { id }, command);
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var allProducts = await _queryHandler.Handle();

            if (allProducts is not null)
            {
                return Ok(allProducts);
            }

            return NotFound("Not Recotd found...");
        }
    }
}