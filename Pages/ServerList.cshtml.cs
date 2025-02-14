using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ServerList.SMOOJsonAPI;
using System.Net;
using System.Net.Sockets;

namespace ServerList.Pages
{
	public class ServerData
	{
		public List<GameServer> SMOO { get; set; } = new List<GameServer>();
		public List<GameServer> Other { get; set; } = new List<GameServer>();
	}

	public class GameServer
	{
		[JsonIgnore]
		public ServerState State { get; set; } = new ServerState();
		public string Title { get; set; }
		public string Address { get; set; }
		public string Port { get; set; }
		public string Hostname { get; set; }
		public DisplayInfo GameInfo { get; set; } = new DisplayInfo();

		public DisplayInfo ServerInfo { get; set; } = new DisplayInfo();
	}

	public class DisplayInfo
	{
		public string Info { get; set; }
		public string Link { get; set; }

		public string linkDisplay { get; set; }
	}
	public class ServerState
	{
		public bool online { get; set; }
		public bool SMOOjsonSuccess { get; set; }
		public List<User> users { get; set; } = new List<User>();
		public int maxUsers { get; set; }
	}
	public class User
	{
		public string name { get; set; }
		public string SMOkingdom { get; set; }
		public string SMOmap { get; set; }
	}
	public class ServerListModel : PageModel
	{
		private readonly ILogger<ServerListModel> _logger;
		public ServerData serverData { get; set; }

		public ServerListModel(ILogger<ServerListModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
			RefreshServers();
		}

		public void RefreshServers()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "servers.json");
			Console.WriteLine(path);
			string json = System.IO.File.ReadAllText(path);
			Console.WriteLine(json);
			serverData = JsonConvert.DeserializeObject<ServerData>(json);
			foreach (GameServer server in serverData.SMOO)
			{
				server.State = SMOOJsonAPI.SMOOJsonAPI.GetSMOData(server, AuthTokens.GetSMOOToken(server));
			}
		}

		public static bool isOnline(GameServer server)
		{
			try
			{
				IPAddress ip = IPAddress.Parse(server.Address);
				int port = int.Parse(server.Port);
				using (TcpClient client = new TcpClient())
				{
					Console.WriteLine($"Pinging {ip}:{port}");
					var result = client.BeginConnect(ip, port, null, null);
					bool success = result.AsyncWaitHandle.WaitOne(500);

					if (success)
					{
						client.EndConnect(result);
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch
			{
				return false;
			}
		}
	}
}