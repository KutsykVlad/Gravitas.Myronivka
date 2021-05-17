using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class Device
        {
            public static void Type(GravitasDbContext context)
            {
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.RfidObidRw, Name = "Rfid. Obid RW02"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.RfidZebraFx9500Head, Name = "Rfid. Zebra Fx9500. Head"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.RfidZebraFx9500Antenna, Name = "Rfid. Zebra Fx9500. Antenna"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.RelayVkmodule2In2Out, Name = "Relay. Vkmodule. 2In2Out"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.RelayVkmodule4In0Out, Name = "Relay. Vkmodule. 4In0Out"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.DigitalIn, Name = "Relay. DigitalIn"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.DigitalOut, Name = "Relay. DigitalOut"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.ScaleMettlerPT6S3, Name = "Scale. Mettler Toledo. PT6S3"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.Led, Name = "Led"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.Camera, Name = "Camera"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.LabFoss, Name = "LabFoss"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.LabBruker, Name = "LabBruker"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.Display, Name = "Display"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.LabInfrascan, Name = "LabInfrascan"});
                context.Set<DeviceType>().AddOrUpdate(new DeviceType {Id = Dom.Device.Type.LabFoss2, Name = "LabFoss2"});
                context.SaveChanges();
            }
        }
    }
}