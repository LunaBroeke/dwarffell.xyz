using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using LunaSite.Blog;
namespace LunaSite.Pages
{
	public class BlogModel : PageModel
    {
		public Root root { get; set; }
        public void OnGet()
        {
			root = GetPosts();
        }
		public static Root GetPosts()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "blogposts.json");
			string json = System.IO.File.ReadAllText(path);
			return JsonConvert.DeserializeObject<Root>(json);
		}
		public static Post GetPost(int index = 0)
		{
			Root r = GetPosts();
			r.Posts = r.Posts.OrderByDescending(p => p.TimeStamp).ToList();
			return r.Posts[index];
		}
		public static string ConvertMarkdownToHtml(string markdown)
		{
			return System.Text.RegularExpressions.Regex.Replace(markdown, @"\[\[(.+?)\]\]",
				match => $"<img src =\"/blogimages/{match.Groups[1].Value}\" alt=\"{match.Groups[1].Value}\" />");
		}
    }
}
