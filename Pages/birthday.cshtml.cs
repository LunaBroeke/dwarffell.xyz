using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace LunaSite.Pages
{
    public class birthdayModel : PageModel
    {
        public void OnGet()
        {
            LunaSite.Register.Register.RefreshTable();
        }

        public string GetHTMLPage()
		{
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(2025, 3, 28);
			DateTime endDate = new DateTime(2025, 3, 29);
			bool theDay = now >= startDate;
            if (now >= endDate)
            {
                return System.IO.File.ReadAllText("Pages/Special/birthdayend.html");
            }
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
