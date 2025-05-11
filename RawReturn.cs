using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
public class RawReturn : ControllerBase
{
    [Route(".well-known/matrix/server")]
    [HttpGet]
    public IActionResult GetMatrixServer()
    {
        var jsonResponse = new Dictionary<string, object>
        {
            { "m.server", "matrix.dwarffell.xyz:443" }
        };

        return new JsonResult(jsonResponse);
    }

    [Route(".well-known/matrix/client")]
    [HttpGet]
    public IActionResult GetMatrixClient()
    {
        var jsonResponse = new Dictionary<string, object>
        {
            { "m.homeserver", new Dictionary<string, string>
                {
                    { "base_url", "https://matrix.dwarffell.xyz" }
                }
            }
        };

        return new JsonResult(jsonResponse);
    }
}