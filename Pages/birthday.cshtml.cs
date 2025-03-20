using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace LunaSite.Pages
{
    public class birthdayModel : PageModel
    {
        public void OnGet()
        {

        }

        public string GetHTMLPage()
		{
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(2025, 3, 19); //Don't forget to change the day to 28 before publish
			DateTime endDate = new DateTime(2025, 3, 29);
			bool theDay = now >= startDate && now <= endDate;
			if (theDay)
            {
                return System.IO.File.ReadAllText("Pages/Special/birthdayhere.html");
            }
            else
            {
                return System.IO.File.ReadAllText("Pages/Special/birthdaywait.html");
            }
		}   
	}
}
