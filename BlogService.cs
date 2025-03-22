using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace LunaSite.Blog
{
	public static class BlogService
	{
		private static readonly string path = "BlogPosts/blogposts.json";

		public static List<Post> GetAllPosts()
		{
			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				Root root = JsonConvert.DeserializeObject<Root>(json);
				return root.Posts;
			}
			else
			{
				return new List<Post>();
			}
		}

		public static Post GetPostById(int id)
		{
			List<Post> posts = GetAllPosts();
			foreach (Post post in posts)
			{
				if (post.id == id)
				{
					return post;
				}
			}
			return null;
		}
		public static void SavePost(Post post)
		{
			List<Post> posts = GetAllPosts();
			posts.Add(post);
			string json = JsonConvert.SerializeObject(new Root { Posts = posts }, Formatting.Indented);
			File.WriteAllText(path, json);
		}
		public static int GetNextPostId()
		{
			List<Post> posts = GetAllPosts();
			return posts.Count+1;
		}
	}
}
