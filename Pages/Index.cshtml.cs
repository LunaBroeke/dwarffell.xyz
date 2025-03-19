using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;
namespace LunaSite.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		//public Post preview;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public string SecretButton()
		{
			DateTime startTime = new DateTime(2025, 3, 19, 23, 15, 00);
			DateTime endTime = new DateTime(2025, 3, 29);
			DateTime now = DateTime.Now;
			bool display = now >= startTime && now <= endTime;
			if (display)
			{
				return "<a href='/birthday'><button class='secret-button'>Secret Button</button></a>";
			}
			else
			{
				return string.Empty;
			}
		}
	}
}