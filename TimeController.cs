using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
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
				string currentTime = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
				return Ok(new { time = currentTime });
			}
			catch (Exception ex)
			{
				Console.WriteLine("oopsie");
				return StatusCode(500, new { message = "Error retrieving time", error = ex.Message });
			}
		}

		[HttpGet("until")]
		public IActionResult GetServerTimeUntil([FromQuery] string targetTime)
		{
			try
			{
				DateTime currentTime = DateTime.Now;
				string[] formats = { "dd-MMMM-yyyy-HH-mm" };
				if (DateTime.TryParseExact(targetTime, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime target))
				{
					TimeSpan timeUntil = target - currentTime;
					if (timeUntil < TimeSpan.Zero)
					{
						return BadRequest(new { message = "Time is in the past" });
					}
					string timeString = "";
					if (timeUntil.Days >= 1)
					{
						timeString = $"{timeUntil.Days} days {timeUntil.Hours} hours {timeUntil.Minutes} minutes and {timeUntil.Seconds} seconds";
					}
					else if (timeUntil.Hours >= 1)
					{
						timeString = $"{timeUntil.Hours} hours {timeUntil.Minutes} minutes and {timeUntil.Seconds} seconds";
					}
					else if (timeUntil.Minutes >= 1)
					{
						timeString = $"{timeUntil.Minutes} minutes and {timeUntil.Seconds} seconds";
					}
					else
					{
						timeString = $"{timeUntil.Seconds} seconds";
					}
						return Ok(new { timeUntil = timeString });
				}
				else
				{
					return BadRequest(new { message = "Invalid time format" });
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("oopsie");
				return StatusCode(500, new { message = "Error retrieving time", error = ex.Message });
			}
		}
	}
}