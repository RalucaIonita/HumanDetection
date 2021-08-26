using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HumanDetection.Controllers
{
    [ApiController]
    [Route("main")]
    public class MainController : Controller
    {
        [HttpGet("return-smth")]
        public IActionResult ReturnBanane()
        {
            return Ok("banane");
        }

        [HttpPost("recognize-human")]
        public IActionResult RecognizeHuman(IFormFile file)
        {

        }
    }
}
