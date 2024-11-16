using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Used to format specific BadRequest errors like
    // /api/example/five instead of
    // /api/example/5
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}