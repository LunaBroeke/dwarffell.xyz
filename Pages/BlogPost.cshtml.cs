using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LunaSite.Blog;
namespace LunaSite.Pages
{
    public class BlogPostModel : PageModel
    {
        public Post Post { get; set; }
		public string images { get; set; }
		public IActionResult OnGet(ulong id)
        {
			Console.WriteLine($"Getting post with id {id}");
			Post = BlogService.GetPostById(id);
			if (Post == null)
			{
				return NotFound();
			}

			if (Post.ImageSources != null && Post.ImageSources.Count >= 1)
			{
				images += "<div class='imageboard'>";
				foreach (string image in Post.ImageSources)
				{
					images += $"<img src='{image}' class='blogimage'/>";
				}
				images += "</div>";
			}
			return Page();
		}
    }
}
