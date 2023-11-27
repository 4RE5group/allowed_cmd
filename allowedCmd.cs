using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO.Compression;
using System.Web;
using System.Threading;
using System.Text.RegularExpressions;

namespace allowedCmd
{
	public class Cmd
	{
		public static void log(string message, string color)
		{
			if (color == "red")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write(message);
				Console.ResetColor();
			}
			else if (color == "green")
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(message);
				Console.ResetColor();
			}
			else if (color == "yellow")
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(message);
				Console.ResetColor();
			}
			else if (color == "orange")
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.Write(message);
				Console.ResetColor();
			}
			else if (color == "blue")
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write(message);
				Console.ResetColor();
			}
		}
		public static void logn(string message, string color)
		{
			if (color == "red")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(message);
				Console.ResetColor();
			}
			else if (color == "green")
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(message);
				Console.ResetColor();
			}
			else if (color == "yellow")
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(message);
				Console.ResetColor();
			}
			else if (color == "blue")
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine(message);
				Console.ResetColor();
			}
			else if (color == "orange")
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine(message);
				Console.ResetColor();
			}
		}
		public static void processShadows(string text, string[] shadow_chars, string color1, string shadow_color)
		{
			foreach(char character in text)
			{
				string character_final=string.Join("", character);
				if(shadow_chars.Contains(character_final))
				{
					// that's a shadow
					log(character_final, shadow_color);
				}
				else
				{
					// that's a text
					log(character_final, color1);
				}
			}
		}
		public static string exec(string cmd)
		{
			ServicePointManager.Expect100Continue = true; 
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc00);
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.Arguments = "/C "+cmd;
			process.StartInfo.WorkingDirectory = cd;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			
			if(runAs)
			{
				process.StartInfo.Verb = "runAs";
			}
			
			process.Start();
			process.WaitForExit();
			
			string line="";
			while (!process.StandardOutput.EndOfStream)
			{
				line += process.StandardOutput.ReadLine();
			}
			return line;
		}
		
		public static string cd;
		public static bool runAs=false;
		
		public static void Main(string[] args)	
		{
			string[] shadow_chars = {"╔", "═", "║", "╝", "╚", "╗"};
			
			Console.WriteLine("");
			processShadows(@"	 █████╗ ██╗     ██╗      ██████╗ ██╗    ██╗███████╗██████╗      ██████╗███╗   ███╗██████╗ 
	██╔══██╗██║     ██║     ██╔═══██╗██║    ██║██╔════╝██╔══██╗    ██╔════╝████╗ ████║██╔══██╗
	███████║██║     ██║     ██║   ██║██║ █╗ ██║█████╗  ██║  ██║    ██║     ██╔████╔██║██║  ██║
	██╔══██║██║     ██║     ██║   ██║██║███╗██║██╔══╝  ██║  ██║    ██║     ██║╚██╔╝██║██║  ██║
	██║  ██║███████╗███████╗╚██████╔╝╚███╔███╔╝███████╗██████╔╝    ╚██████╗██║ ╚═╝ ██║██████╔╝
	╚═╝  ╚═╝╚══════╝╚══════╝ ╚═════╝  ╚══╝╚══╝ ╚══════╝╚═════╝      ╚═════╝╚═╝     ╚═╝╚═════╝ 
", shadow_chars, "yellow", "orange");
Console.WriteLine("                                            made by 4re5 group                                ");
			
			
			string cd = Directory.GetCurrentDirectory();
			
			while(true)
			{
				
				if(runAs)
				{
					log("["+cd+"] #", "red");
				}
				else
				{
					log("["+cd+"] >", "orange");
				}
				string input_cmd = Console.ReadLine();
				
				
				if(input_cmd.StartsWith("sudo "))
				{
					runAs=true;
				}
				
				if(input_cmd.StartsWith("sudo ")) { input_cmd=input_cmd.Substring(5); }
				
				switch(input_cmd)
				{
					case "clear":
						Console.Clear();
						break;
					case "cls":
						Console.Clear();
						break;
					case "exit":
						if(runAs) { runAs=false; }
						break;
						
					default:
						Console.WriteLine(exec(input_cmd));
						break;
				}


			}
		}
	}
}