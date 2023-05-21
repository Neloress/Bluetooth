using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace Receiver // Note: actual namespace depends on the project name.
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string ip = "127.0.0.1";
			int port = 13000;


			IPAddress localAddr = IPAddress.Parse(ip);
			TcpListener server = new TcpListener(localAddr, port);

			server.Start();

			Byte[] bytes = new Byte[1024];

			string message = "";

			while (true) 
			{
				using TcpClient client = server.AcceptTcpClient();
				NetworkStream stream = client.GetStream();

				string data = "";
				int i;
				while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
				{
					data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

					message += data;
				}
				if (message.Last() == '%')
					break;

			}

			message.Remove(message.Length-1, 1);

			List<Tuple<string, string>> files = new List<Tuple<string, string>>();

			string[] splited = message.Split('$');
			foreach (string s in splited)
			{
				string[] splited2 = message.Split('§');
				files.Add(new Tuple<string,string>(splited2[0], splited2[1]));
			}

			string dir = "./../../../../Data/"+ Program.GetTimeStamp();
			Directory.CreateDirectory(dir);

			foreach (Tuple<string, string> tuple in files)
			{
				string path = Path.Combine(dir, tuple.Item1);
				using (StreamWriter sw = File.CreateText(path))
				{
					sw.Write(tuple.Item2);
				}
			}
			Console.Write(message);
		}
		internal static string GetTimeStamp()
		{
			DateTime dateTime = DateTime.Now;

			return dateTime.Year + "." + dateTime.Month + "." + dateTime.Day + "_" + dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + "_" + dateTime.Millisecond;
		}
	}
}