using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Used to test error messages
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public ActionResult NotFoundRequest()
        {
            var thing = _context.Products!.Find(42);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public ActionResult GetServerError()
        {
            var thing = _context.Products!.Find(42);
            var thingToReturn = thing!.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}