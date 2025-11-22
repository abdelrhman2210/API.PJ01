using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API_PJ01_Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")] //GET: baseUrl/api/buggy/notfound
        public IActionResult GetNotFoundResponse()
        {
            // logic
            return NotFound();
        }

        [HttpGet("badrequest")] //GET: baseUrl/api/buggy/badrequest
        public IActionResult GetBadRequestResponse()
        {
            // logic
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")] //GET: baseUrl/api/buggy/badrequest/{id}
        public IActionResult GetValidationErrorResponse(int id)
        {
            // logic
            return BadRequest();
        }

        [HttpGet("servererror")] //GET: baseUrl/api/buggy/servererror
        public IActionResult GetServerErrorResponse()
        {
            // logic
            throw new Exception();
            return BadRequest();
        }

        [HttpGet("unauthorized")] //GET: baseUrl/api/buggy/unauthorized
        public IActionResult GetUnauthorizedResponse()
        {
            // logic
            return Unauthorized();
        }
    }
}
