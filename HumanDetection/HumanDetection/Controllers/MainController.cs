using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.IO;

namespace HumanDetection.Controllers
{
    [ApiController]
    [Route("main")]
    public class MainController : Controller
    {
        private IHumanDetectionService _humanDetectionService { get; set; }

        public MainController(IHumanDetectionService humanDetectionService)
        {
            _humanDetectionService = humanDetectionService;
        }

        [HttpGet("return-smth")]
        public IActionResult ReturnBanane()
        {
            return Ok("banane");
        }

        [HttpPost("recognize-human")]
        public IActionResult RecognizeHuman(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);

            var bytes = stream.ToArray();

            var result = _humanDetectionService.RecognizeHuman(bytes, file.FileName);
            if(result)
                return Ok();
            return BadRequest();
        }


    }
}
