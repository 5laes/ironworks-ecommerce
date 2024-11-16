using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Deriving from this class saves us from manually 
    // checking to see if there are any validation errors
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}