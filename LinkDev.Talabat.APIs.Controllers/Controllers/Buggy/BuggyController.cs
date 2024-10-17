using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")] // GET: /api/Buggy/notfound 
        public IActionResult GetNotFoundRequest()
        {
            //throw new NotFoundException();
            return NotFound(new ApiResponse(404)); // 404
        }

        [HttpGet("servererror")] // GET: /api/Buggy/servererror 
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500
        }
        [HttpGet("badrequest")] // GET: /api/Buggy/badrequest 
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400)); // 400
        }

        [HttpGet("badrequest/{id}")] // GET: /api/Buggy/badrequest/fiv 
        public IActionResult GetValidationError(int id) //-->  400
        {
            return Ok();
        }

        [HttpGet("unauthorized")] // GET: /api/Buggy/unauthorized 
        public IActionResult GetUnauthorized()
        {
            return Unauthorized(new ApiResponse(401)); // 401
        }

        [HttpGet("forbidden")] // GET: /api/Buggy/forbidden 
        public IActionResult GetForbiddenRequest()
        {
            return Forbid(); 
        }

        [Authorize]
        [HttpGet("authorized")] // GET: /api/Buggy/authorized 
        public IActionResult GetAuthorizedRequest()
        {
            return Ok();
        }
    }
}
