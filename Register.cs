using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
namespace LunaSite.Register
{
    public class Table
    {
        public List<Root> RootTable = new List<Root>();
    }
    public class Root
    {
        public string name {get;set;}
        public string message {get;set;}
        public DateTime time {get;set;}
    }
    [Route("api/register")]
    [ApiController]
    public class Register : ControllerBase
    {
        private static Table table = new Table();
        private static List<string> ips = new List<string>();
        private static DateTime refreshTime = new DateTime(2025, 3, 19);
        [HttpPost("bday")]
        public IActionResult RegisterBday([FromBody] Root root)
        {
            RefreshTable();
            if (refreshTime < DateTime.Now)
            {
                refreshTime = DateTime.Now.AddMinutes(10);
                Console.WriteLine($"Refresh time has been moved to {refreshTime}");
                ips.Clear();
            }
            if (root == null)
            {
                Console.WriteLine("Root is null");
                return BadRequest( new {message = "Invalid Request"});
            }
            if (root == null || string.IsNullOrWhiteSpace(root.name))
            {
                return BadRequest(new {message = "Name is required"});
            }
            if (string.IsNullOrWhiteSpace(root.message))
            {
                root.message = "";
            }

            string ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"IP: {ip}");
            foreach (string i in ips)
            {
                Console.WriteLine($"Stored IP: {i}");
            }
            if (ips.Contains(ip))
            {
                return BadRequest(new {message = "You have already submitted a message recently!"});
            }
            ips.Add(ip);
            root.time = DateTime.Now;

            table.RootTable.Add(root);
            Console.WriteLine($"Name {root.name}, Message: {root.message}, Time: {root.time}");
            WriteTable();
            return Ok(new {message = "Thank you for your submission! Please check the bottom of the page to see your message!"});
        }
        [HttpGet("bdaytable")]
        public IActionResult DisplayBdayTable()
        {
            string s = "";
            foreach (Root r in table.RootTable.AsEnumerable().Reverse())
            {
                s += $"<div class='bdaypost'><span class='bdayname'>{r.name} Wishes Luna a Happy Birthday!</span><br/><span class='bdaymessage'>{r.message}</span><br/><span class='bdaytime'>{r.time.ToString("HH:mm:ss")}</span></div>";   
            }
            return Ok( new {message = $"<div class='bdaymessagetable'>{s}</div>"});
        }

        public static void RefreshTable()
		{
			string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "bdayTable.json");
            try
            {
			Console.WriteLine($"Requesting {path}");
			string json = System.IO.File.ReadAllText(path);
			table = JsonConvert.DeserializeObject<Table>(json);
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine($"Tried to request {path} but it doesn't exist (yet)");
            }
		}

        public static void WriteTable()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "bdayTable.json");
            Console.WriteLine($"Writing to {path}");

            try
            {
                string json = JsonConvert.SerializeObject(table, Formatting.Indented);
                System.IO.File.WriteAllText(path, json);
                Console.WriteLine("Table written successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing table: {ex.Message}");
            }
        }
    }
}