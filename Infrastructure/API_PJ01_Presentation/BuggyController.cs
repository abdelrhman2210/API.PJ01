using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API_PJ01_Presentation
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet(template: "notfound")] //GET: baseUrl/api/buggy/notfound
        public IActionResult GetNotFoundResponse()
        {
            // logic
            return NotFound();
        }

        [HttpGet(template: "badrequest")] //GET: baseUrl/api/buggy/badrequest
        public IActionResult GetBadRequestResponse()
        {
            // logic
            return BadRequest();
        }

        [HttpGet(template: "badrequest/{id}")] //GET: baseUrl/api/buggy/badrequest/{id}
        public IActionResult GetValidationErrorResponse(int id)
        {
            // logic
            return BadRequest();
        }

        [HttpGet(template: "servererror")] //GET: baseUrl/api/buggy/servererror
        public IActionResult GetServerErrorResponse()
        {
            // logic
            throw new Exception();
            return BadRequest();
        }

        [HttpGet(template: "unauthorized")] //GET: baseUrl/api/buggy/unauthorized
        public IActionResult GetUnauthorizedResponse()
        {
            // logic
            return Unauthorized();
        }
    }
}
