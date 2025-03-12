using Microsoft.AspNetCore.Mvc;
using System;
namespace LunaSite
{
    [Route("api/time")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetServerTime()
        {
            try
            {
                var currentTime = DateTime.Now.ToString("HH:mm:ss");
                return Ok(new { time = currentTime });
            }
            catch (Exception ex)
            {
                Console.WriteLine("oopsie");
                return StatusCode(500, new { message = "Error retrieving time", error = ex.Message});
            }
        }
    }
}