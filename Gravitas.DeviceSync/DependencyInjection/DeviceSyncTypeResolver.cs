using Gravitas.Core.Manager;
using Gravitas.DAL;
using Gravitas.DeviceSync.Manager;
using Gravitas.DeviceSync.Manager.RfidZebraFx9500;
using Gravitas.DeviceSync.Manager.VkModuleSocket2;
using Gravitas.Infrastructure.Platform.DependencyInjection;
using Unity;

namespace Gravitas.DeviceSync.DependencyInjection
{
    public class DeviceSyncTypeResolver : ITypeResolver
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<GravitasDbContext, GravitasDbContext>();

            container.RegisterType<ICardRepository, CardRepository>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<INodeRepository, NodeRepository>();

            container.RegisterType<IOpDataRepository, OpDataRepository>();

            container.RegisterType<IEmployeeRolesRepository, EmployeeRolesRepository>();

            container.RegisterType<IDeviceSyncManager, DeviceSyncManager>();

            container.RegisterType<IRfidObidRwManager, RfidObidRwManager>();
            container.RegisterType<IRfidZebraFx9500Manager, RfidZebraFx9500Manager>();
            container.RegisterType<IVkModuleSocket1Manager, VkModuleSocket1Manager>();
            container.RegisterType<IVkModuleSocket2Manager, VkModuleSocket2Manager>();
            container.RegisterType<IScaleMettlerPT6S3Manager, ScaleMettlerPT6S3Manager>();
            container.RegisterType<ILabBrukerManager, LabBrukerManager>();
            container.RegisterType<ILabFossManager, LabFossManager>();
            container.RegisterType<ILabFossManager2, LabFossManager2>();
            container.RegisterType<ILabInfrascanManager, LabInfrascanManager>();
        }
    }
}