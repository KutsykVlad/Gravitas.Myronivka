using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravitas.Platform.Web.SignalR;
using Microsoft.AspNet.SignalR;

namespace SignalRDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			MyHub hub = new MyHub();
			while (true) {
				//hub.Hello();

				var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
				context.Clients.All.hello();

				Console.ReadKey();
			}
		}
	}
}
