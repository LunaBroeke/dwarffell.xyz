using System.Collections.Generic;
using ServerList;
namespace LunaSite.Blog
{
    public class Post
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public string Author { get; set; }
        public List<string> ImageSources { get; set; }
		public DateTime TimeStamp { get; set; }
	}

	public class Root
	{
		public List<Post> Posts { get; set; }
	}

    public class WriteRoot
    {
        public string user {get; set;} = AuthTokens.AdminUser;
        public string pass {get; set;} = AuthTokens.AdminPass;
        public Post? post {get; set;}
		public string ToString()
		{
			if (post == null)
			{ return $"{user}:{pass}";}
			else
			{ return $"{user}:{pass}";}
		}
    }
}