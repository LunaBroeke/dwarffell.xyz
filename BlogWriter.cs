using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using LunaSite.Blog;
namespace LunaSite
{
    [Route("api/admin/blogwriter")]
    [ApiController]
    public class BlogWriter : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult WritePost([FromBody] WriteRoot writeRoot)
        {
            Console.WriteLine(writeRoot.ToString());
            WriteRoot verify = new WriteRoot();
            if (writeRoot.user == verify.user && writeRoot.pass == verify.pass)
            {
                return Ok("User Authorized");
            }
            else
            {
                Console.WriteLine($"User tried to login with {writeRoot.user}:{writeRoot.pass} while expecting {verify.user}:{verify.pass}");
                return BadRequest("User Invalid");
            }
        }
    }
}