using System;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Microsoft.AspNet.SignalR.Client;


namespace SignalRClientDemo
{
	class Program
	{
		static void Main(string[] args)
		{

			//while (true)
			//{
			//	try {
			//		var connection = new HubConnection("http://localhost:3142/");
			//		IHubProxy myHub = connection.CreateHubProxy("NodeHub");
			//		connection.Start().Wait();

			//		myHub.Invoke("Reload", "1");


			//		Console.ReadKey();
			//	}
			//	catch (Exception e)
			//	{
			//		Console.WriteLine(e);
			//	}
			//}


			while (true)
			{
				try {
					var nodeHubClient = new SignalRClient.MainHub("http://localhost:3142/");
					nodeHubClient.Reload("1");
					Console.WriteLine("Group 1 Reload");
					Console.ReadKey();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}
