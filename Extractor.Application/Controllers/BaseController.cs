using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Extractor.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]

    public class BaseController : ControllerBase
    {
        [HttpGet("v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult Get() => Ok("BaseController");
    }
}
