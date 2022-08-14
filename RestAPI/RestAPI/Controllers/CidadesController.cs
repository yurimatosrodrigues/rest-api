using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : Controller
    {
       [HttpGet("hello-world")]
       public string Hello()
        {
            return "Hello World!";
        }
    }
}
