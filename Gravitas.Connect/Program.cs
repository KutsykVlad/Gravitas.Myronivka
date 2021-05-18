using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Gravitas.Connect.Manager;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Topshelf;

namespace Gravitas.Connect
{
	public class Service {
		
		public static event EventHandler StartAllEvent = delegate {};
		public static event EventHandler StartLightEvent = delegate {};
		
		[ServiceContract]
		public interface IService
		{
			[OperationContract]
			[WebGet]
			HttpStatusCode StartAll(string dictionary);
			
			[OperationContract]
			[WebGet]
			HttpStatusCode StartLight(string dictionary);
		}

		private class ServiceApi : IService
		{
			public HttpStatusCode StartLight(string dictionary)
			{
				StartLightEvent(null, EventArgs.Empty);
				return HttpStatusCode.OK;
			}
			
			public HttpStatusCode StartAll(string dictionary)
			{
				StartAllEvent(null, EventArgs.Empty);
				return HttpStatusCode.OK;
			}
		}
		
		private readonly Api1CConnectManager _manager;
		private static readonly object Locker = new object();
		private readonly WebServiceHost _host = new WebServiceHost(typeof(ServiceApi), new Uri("http://localhost:8088/Connect/"));
		public Service(Api1CConnectManager manager) {
			_manager = manager;
			
			StartLightEvent += (sender, args) =>
			{
				Task.Run(() => { StartSync(true); });
			};
			
			StartAllEvent += (sender, args) =>
			{
				Task.Run(() => { StartSync(false); });
			};
		}

		private void StartSync(bool noDbStore)
		{
			lock (Locker)
			{
				_manager.SyncDictionaries(noDbStore);
			}
		}

		public void Start() 
		{ 
			try
			{
				_host.Open();
			}
			catch (CommunicationException cex)
			{
				Console.WriteLine("An exception occurred in Connect service: {0}", cex.Message);
				_host.Abort();
			} 
		}

		public void Stop()
		{
//			_timer.Stop();
			_host.Close();
		}
	}

	static class Program {
		
		static void Main()
		{
			Bootstrapper.Initialize();

			Api1CConnectManager manager =
				new Api1CConnectManager(new ExternalDataRepository(new GravitasDbContext()));

			var rc = HostFactory.Run(x => {

				x.Service<Service>(s => {

					s.ConstructUsing(name => new Service(manager));
					s.WhenStarted(tc => tc.Start());
					s.WhenStopped(tc => tc.Stop());
				});
				x.RunAsLocalSystem();

				x.SetDescription("Gravitas.Connect");
				x.SetDisplayName("Gravitas.Connect");
				x.SetServiceName("Gravitas.Connect");
			});

			var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
			Environment.ExitCode = exitCode;
		}
	}
}
