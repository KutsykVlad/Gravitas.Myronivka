using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Unity;

namespace Gravitas.DeviceSync.DependencyInjection
{
	public class ConnectTypeResolver : ITypeResolver
	{
		public void RegisterType(IUnityContainer container) {
			container.RegisterType<GravitasDbContext, GravitasDbContext>();
			
			container.RegisterType<ICardRepository, CardRepository>();
			container.RegisterType<IDeviceRepository, DeviceRepository>();
			container.RegisterType<INodeRepository, NodeRepository>();
		    container.RegisterType<ISmsTemplatesRepository, SmsTemplatesRepository>();

		    container.RegisterType<IEmployeeRolesRepository, EmployeeRolesRepository>();

			container.RegisterType<IOpDataRepository, OpDataRepository>();
			container.RegisterType<IExternalDataRepository, ExternalDataRepository>();

			container.RegisterType<ISmsMobizonApiClient, SmsMobizonApiClient>();
		}
	}
}