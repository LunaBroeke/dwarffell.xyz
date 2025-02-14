using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LunaSite.Pages
{
    public class WIPModel : PageModel
    {
        public void OnGet()
        {
        }
        public static string WipText()
        {
            return "I'll get to it soon...";
        }
    }
}
