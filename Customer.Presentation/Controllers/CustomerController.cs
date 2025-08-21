using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");

        }
    }
}
