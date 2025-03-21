using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LunaSite.Blog;
namespace LunaSite.Pages
{
    public class BlogPostModel : PageModel
    {
        public Post Post { get; set; }
		public IActionResult OnGet(ulong id)
        {
			Post = BlogService.GetPostById(id);
			if (Post == null)
			{
				return NotFound();
			}
			return Page();
		}
    }
}
