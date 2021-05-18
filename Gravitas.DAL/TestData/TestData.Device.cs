using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.TestData {

	public static partial class TestData {

		public static void Device(GravitasDbContext context) {

			#region MyRegion

			context.Set<Device>().AddOrUpdate(new Device { Id = 1, ParentDeviceId = null, ParamId = 1, StateId = 1, TypeId = Dom.Device.Type.RfidObidRw, IsActive = false, Name = "Test Obid Rfid" });

			context.Set<Device>().AddOrUpdate(new Device { Id = 2, ParentDeviceId = null, ParamId = 2, StateId = 2, TypeId = Dom.Device.Type.RelayVkmodule2In2Out, IsActive = true, Name = "Test VkModule Socket 2" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 3, ParentDeviceId = 2, ParamId = 3, StateId = 3, TypeId = Dom.Device.Type.DigitalIn, IsActive = true, Name = "Test VkModule Socket 2. DI 1" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 4, ParentDeviceId = 2, ParamId = 4, StateId = 4, TypeId = Dom.Device.Type.DigitalIn, IsActive = true, Name = "Test VkModule Socket 2. DI 2" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 5, ParentDeviceId = 2, ParamId = 5, StateId = 5, TypeId = Dom.Device.Type.DigitalOut, IsActive = true, Name = "Test VkModule Socket 2. DO 1" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 6, ParentDeviceId = 2, ParamId = 6, StateId = 6, TypeId = Dom.Device.Type.DigitalOut, IsActive = true, Name = "Test VkModule Socket 2. DO 2" });
			
			//context.Set<Device>().AddOrUpdate(new Device { Id = 7, ParentDeviceId = null, ParamId = 7, StateId = 7, TypeId = Dom.Device.Type.RfidZebraFx9500Head, IsActive = true, Name = "Zebra FX9500" });
			//context.Set<Device>().AddOrUpdate(new Device { Id = 8, ParentDeviceId = 7, ParamId = 8, StateId = 8, TypeId = Dom.Device.Type.RfidZebraFx9500Antenna, IsActive = true, Name = "Zebra FX9500; Antenna 1" });
			//context.Set<Device>().AddOrUpdate(new Device { Id = 9, ParentDeviceId = 7, ParamId = 9, StateId = 9, TypeId = Dom.Device.Type.RfidZebraFx9500Antenna, IsActive = true, Name = "Zebra FX9500; Antenna 2" });
			//context.Set<Device>().AddOrUpdate(new Device { Id = 10, ParentDeviceId = 7, ParamId = 10, StateId = 10, TypeId = Dom.Device.Type.RfidZebraFx9500Antenna, IsActive = true, Name = "Zebra FX9500; Antenna 3" });
			//context.Set<Device>().AddOrUpdate(new Device { Id = 11, ParentDeviceId = 7, ParamId = 11, StateId = 11, TypeId = Dom.Device.Type.RfidZebraFx9500Antenna, IsActive = true, Name = "Zebra FX9500; Antenna 4" });

			context.Set<Device>().AddOrUpdate(new Device { Id = 12, ParentDeviceId = null, ParamId = 12, StateId = 12, TypeId = Dom.Device.Type.Camera, IsActive = true, Name = "Camera" });

			context.Set<Device>().AddOrUpdate(new Device { Id = 13, ParentDeviceId = null, ParamId = 13, StateId = 13, TypeId = Dom.Device.Type.ScaleMettlerPT6S3, IsActive = false, Name = "Scale. Mettler Toledo" });

			context.Set<Device>().AddOrUpdate(new Device { Id = 15, ParentDeviceId = null, ParamId = 15, StateId = 15, TypeId = Dom.Device.Type.ScaleMettlerPT6S3, IsActive = false, Name = "Scale. Mettler Toledo" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 16, ParentDeviceId = null, ParamId = 16, StateId = 16, TypeId = Dom.Device.Type.ScaleMettlerPT6S3, IsActive = false, Name = "Scale. Mettler Toledo" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 17, ParentDeviceId = null, ParamId = 17, StateId = 17, TypeId = Dom.Device.Type.ScaleMettlerPT6S3, IsActive = false, Name = "Scale. Mettler Toledo" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 18, ParentDeviceId = null, ParamId = 18, StateId = 18, TypeId = Dom.Device.Type.ScaleMettlerPT6S3, IsActive = false, Name = "Scale. Mettler Toledo" });

			context.Set<Device>().AddOrUpdate(new Device { Id = 20, ParentDeviceId = null, ParamId = 20, StateId = 20, TypeId = Dom.Device.Type.RelayVkmodule4In0Out, IsActive = true, Name = "Test VkModule Socket 4" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 21, ParentDeviceId = 20, ParamId = 21, StateId = 21, TypeId = Dom.Device.Type.DigitalIn, IsActive = true, Name = "Test VkModule Socket 4. DI 1" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 22, ParentDeviceId = 20, ParamId = 22, StateId = 22, TypeId = Dom.Device.Type.DigitalIn, IsActive = true, Name = "Test VkModule Socket 4. DI 2" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 23, ParentDeviceId = 20, ParamId = 23, StateId = 23, TypeId = Dom.Device.Type.DigitalIn, IsActive = true, Name = "Test VkModule Socket 4. DI 3" });
			context.Set<Device>().AddOrUpdate(new Device { Id = 24, ParentDeviceId = 20, ParamId = 24, StateId = 24, TypeId = Dom.Device.Type.DigitalIn, IsActive = true, Name = "Test VkModule Socket 4. DI 4" });
			

			#endregion
			int devId = 0;

			#region 02.01.01  КПП №1 верхняя территория  ЛВС02
			devId = 1000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Камера №1 Виїзд"
			});

			devId = 1000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Камера №2 Вїзд"
			});

			devId = 1000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Охорона в'їзд"
			});

			devId = 1000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Охорона виїзд"
			});

			devId = 1000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Голова. Охорона в'їзд"
			});

			devId = 1000501;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = 1000500,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
				IsActive = true,
				Name = "RFID Zebra. Антена. Охорона в'їзд"
			});

			devId = 1000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Охорона настільний"
			});

			devId = 1000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд"
			});

						devId = 1000701;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 1"
						});

						devId = 1000702;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 2"
						});

						devId = 1000703;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 1"
						});

						devId = 1000704;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 2"
						});

			devId = 1000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд"
			});

						devId = 1000801;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 1"
						});

						devId = 1000802;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 2"
						});

						devId = 1000803;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 1"
						});

						devId = 1000804;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 2"
						});
						
			devId = 1000900;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація"
			});

						devId = 1000901;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000900,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 1"
						});

						devId = 1000902;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000900,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 2"
						});

						devId = 1000903;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000900,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 1"
						});

						devId = 1000904;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1000900,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 2"
						});
						
			devId = 1001000;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Виїзд валідація"
			});

						devId = 1001001;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1001000,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 1"
						});

						devId = 1001002;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1001000,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. In 2"
						});

						devId = 1001003;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1001000,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 1"
						});

						devId = 1001004;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 1001000,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд. Out 2"
						});
			context.SaveChanges();
			#endregion

			#region 04.01.01  Авто №4 верхняя территория 
			devId = 3000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.ScaleMettlerPT6S3,
				IsActive = true,
				Name = "Ваги №1"
			});

			devId = 3000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ваги №1"
			});

			devId = 3000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Ваги 1."
			});

						devId = 3000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 1. In 1"
						});

						devId = 3000402;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 1. In 2"
						});

						devId = 3000403;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 1. In 3"
						});

						devId = 3000404;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 1. In 4"
						});

			devId = 3000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Ваги 1."
			});

						devId = 3000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 1. In 1"
						});

						devId = 3000502;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 1. In 2"
						});

						devId = 3000503;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 1. Out 1"
						});

						devId = 3000504;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 3000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 1. Out 2"
						});

			devId = 3000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 1. Камера (кузов)"
			});

			devId = 3000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 1. Камера (заїзд)"
			});

			devId = 3000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 1. Камера (виїзд)"
			});

			context.SaveChanges();
			#endregion

			#region 04.01.02  Авто №2 верхняя территория 
			devId = 4000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.ScaleMettlerPT6S3,
				IsActive = true,
				Name = "Ваги №2"
			});

			devId = 4000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ваги №2"
			});

			devId = 4000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Ваги 2."
			});

						devId = 4000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 2. In 1"
						});

						devId = 4000402;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 2. In 2"
						});

						devId = 4000403;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 2. In 3"
						});

						devId = 4000404;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 2. In 4"
						});

			devId = 4000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Ваги 2."
			});

						devId = 4000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 2. In 1"
						});

						devId = 4000502;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 2. In 2"
						});

						devId = 4000503;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 2. Out 1"
						});

						devId = 4000504;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 4000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 2. Out 2"
						});

			devId = 4000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 2. Камера (кузов)"
			});

			devId = 4000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 2. Камера (заїзд)"
			});

			devId = 4000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 2. Камера (виїзд)"
			});

			context.SaveChanges();
			#endregion

			#region 04.01.03  Авто №3 верхняя территория 
			devId = 5000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.ScaleMettlerPT6S3,
				IsActive = true,
				Name = "Ваги №3"
			});

			devId = 5000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ваги №3"
			});

			devId = 5000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Ваги 3."
			});

						devId = 5000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 3. In 1"
						});

						devId = 5000402;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 3. In 2"
						});

						devId = 5000403;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 3. In 3"
						});

						devId = 5000404;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 3. In 4"
						});

			devId = 5000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Ваги 3."
			});

						devId = 5000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 3. In 1"
						});

						devId = 5000502;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 3. In 2"
						});

						devId = 5000503;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 3. Out 1"
						});

						devId = 5000504;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 5000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 3. Out 2"
						});

			devId = 5000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 3. Камера (кузов)"
			});

			devId = 5000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 3. Камера (заїзд)"
			});

			devId = 5000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 3. Камера (виїзд)"
			});

			context.SaveChanges();
			#endregion

			#region 04.01.04  Авто №4 верхняя территория 
			devId = 6000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.ScaleMettlerPT6S3,
				IsActive = true,
				Name = "Ваги №4"
			});

			devId = 6000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ваги №4"
			});

			devId = 6000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Ваги 4."
			});

						devId = 6000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 4. In 1"
						});

						devId = 6000402;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 4. In 2"
						});

						devId = 6000403;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 4. In 3"
						});

						devId = 6000404;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 4. In 4"
						});

			devId = 6000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Ваги 4."
			});

						devId = 6000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 4. In 1"
						});

						devId = 6000502;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 4. In 2"
						});

						devId = 6000503;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 4. Out 1"
						});

						devId = 6000504;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 6000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 4. Out 2"
						});

			devId = 6000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 4. Камера (кузов)"
			});

			devId = 6000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 4. Камера (заїзд)"
			});

			devId = 6000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 4. Камера (виїзд)"
			});

			context.SaveChanges();
			#endregion

			#region Візіровка №0
			
			devId = 7000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Візіровка №0"
			});

			context.SaveChanges();
			#endregion
			
			#region Відбір проб 1
			
			devId = 15000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Підписи оператора(Відбору шроту)"
			});
			
			devId = 15000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Зчитувач транспорту(Відбору шроту 1)"
			});

			context.SaveChanges();
			#endregion
			
			#region 03.01.01  Візіровка №3 верхняя территория 
			
			devId = 800300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Візіровка №3 "
			});
			devId = 800500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Голова. Візіровка №3 "
			});
				devId = 800501;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 800500,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
					IsActive = true,
					Name = "RFID Zebra. Антена. Візіровка №3 "
				});
			
			context.SaveChanges();
			#endregion

			#region 03.01.02  Візіровка №4 верхняя территория 

			devId = 9000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Візіровка №4 "
			});

			devId = 9000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Голова. Візіровка №4 "
			});

				devId = 9000501;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 9000500,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
					IsActive = true,
					Name = "RFID Zebra. Антена. Візіровка №4 "
				});

			#endregion

			#region 03.01.0X  Візіровка - Лабораторія 

			devId = 9000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.LabFoss,
				IsActive = true,
				Name = "Foss. Лабораторія (візіровка)"
			});
			
			devId = 9000101;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.LabFoss2,
				IsActive = true,
				Name = "Foss2. Лабораторія (візіровка)"
			});

			devId = 9000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.LabBruker,
				IsActive = true,
				Name = "Bruker. Лабораторія (візіровка)"
			});
			
			devId = 9000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.LabInfrascan,
				IsActive = true,
				Name = "Infrascan. Лабораторія (візіровка)"
			});
			context.SaveChanges();
			#endregion

			#region 01.01.01  Єдине вікно

			devId = 10000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Єдине вікно "
			});
			devId = 10000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Єдине вікно "
			});
			devId = 10000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Єдине вікно "
			});

			context.SaveChanges();
			#endregion

			#region 03.05.04  Елеватор №4 та Елеватор №5
			
			devId = 11000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Головна Операторська"
			});

			devId = 11000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма розгрузки 100"
			});

						devId = 11000301;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000300,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Яма розгрузки 100"
						});

			devId = 11000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма розгрузки 200"
			});

						devId = 11000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Яма розгрузки 200"
						});

			devId = 11000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма розгрузки 300"
			});

						devId = 11000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Яма розгрузки 300"
						});

			devId = 11000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 100"
			});

						devId = 11000601;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 100. In 1"
						});

						devId = 11000602;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 100. In 2"
						});

						devId = 11000603;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 100. Out 1"
						});

						devId = 11000604;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 100. Out 2"
						});

			devId = 11000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 200"
			});

						devId = 11000701;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 200. In 1"
						});

						devId = 11000702;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 200. In 2"
						});

						devId = 11000703;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 200. Out 1"
						});

						devId = 11000704;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000700,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 200. Out 2"
						});

			devId = 11000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 300"
			});

						devId = 11000801;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 300. In 1"
						});

						devId = 11000802;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 300. In 2"
						});

						devId = 11000803;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 300. Out 1"
						});

						devId = 11000804;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 11000800,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 300. Out 2"
						});
			
			devId = 11000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Елеватор №4,5 Завантаження №1"
			});
			
			devId = 11000201;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Елеватор №4,5 Завантаження №2"
			});
			
			devId = 11000202;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Елеватор №4,5 Завантаження №3"
			});

			context.SaveChanges();
			#endregion
				
			#region 03.03.02  Елеватор №3
			
			devId = 13000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Головна Операторська"
			});

			devId = 13000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма розгрузки 30"
			});

						devId = 13000201;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 13000200,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Яма розгрузки 30"
						});

			devId = 13000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма розгрузки 40"
			});

						devId = 13000301;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 13000300,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Яма розгрузки 40"
						});

	
			devId = 13000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 30"
			});

						devId = 13000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 13000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 30. In 1"
						});

						devId = 13000402;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 13000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 40. In 2"
						});

						devId = 13000403;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 13000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 30. Out 1"
						});

						devId = 13000404;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 13000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 40. Out 2"
						});

			devId = 13000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Елеватор №2 Завантаження зернових №1"
			});
			
			devId = 13000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Елеватор №2 Завантаження зернових №2"
			});

			context.SaveChanges();
			#endregion
					
			#region 03.02.02  Елеватор №2
			
			devId = 14000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Головна Операторська"
			});
			
			devId = 14000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма розгрузки 50"
			});

						devId = 14000201;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000200,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Яма розгрузки 50"
						});

			devId = 14000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Виїзд на поле"
			});

						devId = 14000301;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000300,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Виїзд на поле"
						});

			devId = 14000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Виїзд з поля"
			});

						devId = 14000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
							IsActive = true,
							Name = "RFID Zebra. Антена. Виїзд з поля"
						});

	
	
			devId = 14000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 50"
			});

						devId = 14000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 50. In 1"
						});

						devId = 14000502;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 50. In 2"
						});

						devId = 14000503;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 50. Out 1"
						});

						devId = 14000504;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Вїзд Яма розгрузки 50. Out 2"
						});

	
			devId = 14000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Виїзд з поля"
			});

						devId = 14000601;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Виїзд з поля. in 1 "
						});

						devId = 14000602;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Виїзд з поля. In 2"
						});

						devId = 14000603;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Виїзд з поля. Out 1"
						});

						devId = 14000604;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 14000600,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Виїзд з поля. Out 2"
						});

			

			context.SaveChanges();
			#endregion
			
			#region Призначення ями вигрузки

			devId = 12000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Призначення ями вигрузки 1"
			});
			context.SaveChanges();
			
			devId = 12000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Призначення ями вигрузки 2"
			});
			
			context.SaveChanges();
            #endregion

            #region Черга

		    devId = 13000001;
		    context.Set<Device>().AddOrUpdate(new Device
		    {
		        Id = devId,
		        ParentDeviceId = null,
		        ParamId = devId,
		        StateId = null,
		        TypeId = Dom.Device.Type.Display,
		        IsActive = true,
		        Name = "Табло. Єдине вікно"
		    });

		    devId = 13000002;
		    context.Set<Device>().AddOrUpdate(new Device
		    {
		        Id = devId,
		        ParentDeviceId = null,
		        ParamId = devId,
		        StateId = null,
		        TypeId = Dom.Device.Type.Display,
		        IsActive = true,
		        Name = "Табло. 2"
		    });
		    
		    devId = 13000003;
		    context.Set<Device>().AddOrUpdate(new Device
		    {
			    Id = devId,
			    ParentDeviceId = null,
			    ParamId = devId,
			    StateId = null,
			    TypeId = Dom.Device.Type.Display,
			    IsActive = true,
			    Name = "Табло. 3"
		    });
            context.SaveChanges();
            #endregion
			
			#region Центральна лабораторія
			
			devId = 16000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Центральна лабораторія №1"
			});
			
			devId = 16000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Центральна лабораторія №2"
			});
			
			devId = 16000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Центральна лабораторія №3"
			});

			context.SaveChanges();
			#endregion
			
			#region Комбікормовий завод
			
			devId = 17000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Комбікормовий завод. Підписи оператора"
			});
			
			devId = 17100100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Комбікормовий завод. Проїзд 1"
			});
			
			devId = 17100200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Комбікормовий завод. Проїзд 2"
			});
			
			devId = 17100300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Комбікормовий завод. Проїзд 3"
			});
			
			devId = 17100400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Комбікормовий завод. Проїзд 4"
			});
			
			devId = 17100900;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Комбікормовий завод. Майстер"
			});			
			
			devId = 17100110;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100111;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100110,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 3. In 0"
				});
			
				devId = 17100112;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100110,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 3. In 1"
				});
				
				devId = 17100113;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100110,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 1. In 2"
				});
				
				devId = 17100114;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100110,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 5. In 3"
				});
			
			devId = 17100120;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100121;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100120,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 2. In 0"
				});
			
				devId = 17100122;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100120,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 6. In 1"
				});
				
				devId = 17100123;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100120,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 4. In 2"
				});
				
				devId = 17100124;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100120,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 4. In 3"
				});
			
			devId = 17100130;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100131;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100130,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 9. In 0"
				});
			
				devId = 17100132;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100130,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 9. In 1"
				});
				
				devId = 17100133;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100130,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 7. In 2"
				});
				
				devId = 17100134;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100130,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 11. In 3"
				});
			
			devId = 17100140;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100141;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100140,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 8. In 0"
				});
			
				devId = 17100142;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100140,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 12. In 1"
				});
				
				devId = 17100143;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100140,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 10. In 2"
				});
				
				devId = 17100144;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100140,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 10. In 3"
				});
			
			devId = 17100150;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100151;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100150,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 15. In 0"
				});
			
				devId = 17100152;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100150,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 15. In 1"
				});
				
				devId = 17100153;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100150,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 13. In 2"
				});
				
				devId = 17100154;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100150,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 17. In 3"
				});
			
			devId = 17100160;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100161;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100160,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 14. In 0"
				});
			
				devId = 17100162;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100160,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 18. In 1"
				});
				
				devId = 17100163;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100160,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 16. In 2"
				});
				
				devId = 17100164;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100160,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 16. In 3"
				});
			
			devId = 17100170;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100171;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100170,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 21. In 0"
				});
			
				devId = 17100172;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100170,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 21. In 1"
				});
				
				devId = 17100173;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100170,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 19. In 2"
				});
				
				devId = 17100174;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100170,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 23. In 3"
				});	
			
			devId = 17100180;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Комбікормовий завод. Силоси."
			});

				devId = 17100181;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100180,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 20. In 0"
				});
			
				devId = 17100182;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100180,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 24. In 1"
				});
				
				devId = 17100183;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100180,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 22. In 2"
				});
				
				devId = 17100184;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100180,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Силос 22. In 3"
				});
			
			devId = 17100210;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Петля, аварія."
			});

				devId = 17100211;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100210,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Петля 1. In 0"
				});
	
				devId = 17100212;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100210,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Петля 2. In 1"
				});
	
				devId = 17100213;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100210,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Аварійний вимикач 1. Out 0"
				});
	
				devId = 17100214;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100210,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Аварійний вимикач 2. Out 1"
				});

			devId = 17100220;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Петля, аварія."
			});

				devId = 17100221;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100220,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Петля 3. In 0"
				});
		
				devId = 17100222;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100220,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Петля 4. In 1"
				});
		
				devId = 17100223;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100220,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Аварійний вимикач 3. Out 0"
				});
		
				devId = 17100224;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17100220,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Аварійний вимикач 4. Out 1"
				});
				
			devId = 17200100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація СГП 1"
			});

				devId = 17200101;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 17200102;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 17200103;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 17200104;
				context.Set<Device>().AddOrUpdate(new Device
				{
					Id = devId,
					ParentDeviceId = 17200100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});

				devId = 17200200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація СГП 2"
			});

				devId = 17200201;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 17200202;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 17200203;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 17200204;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
		
			devId = 17200300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація СГП 3"
			});

				devId = 17200301;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 17200302;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 17200303;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 17200304;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 17200400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація СГП 4"
			});

				devId = 17200401;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 17200402;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 17200403;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 17200404;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 17200400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
			
			context.SaveChanges();
			#endregion
			
			#region Елеватор 1

			devId = 19000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Оператор елеватор 1"
			});
			
			devId = 19000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Голова. Яма схема 5"
			});
			
			devId = 19000201;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = 19000200,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
				IsActive = true,
				Name = "RFID Zebra. Антена. Яма схема 5"
			});
			
			devId = 19000210;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Head,
				IsActive = true,
				Name = "RFID Zebra. Яма ККЗ"
			});
			
			devId = 19000211;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = 19000210,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidZebraFx9500Antenna,
				IsActive = true,
				Name = "RFID Zebra. Антена. Яма ККЗ"
			});
			
			devId = 19000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Яма ККЗ, Яма схема 5"
			});

				devId = 19000301;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 19000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Яма схема 5. In 1"
				});
	
				devId = 19000302;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 19000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Яма ККЗ. In 2"
				});
	
				devId = 19000303;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 19000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Яма ККЗ. Out 2"
				});
	
				devId = 19000304;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 19000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Яма схема 5. Out 1"
				});
			
			context.SaveChanges();
			#endregion
			
			#region Шрот

			devId = 20000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Оператор погрузки шрота"
			});
			
			devId = 20000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки шрота 1"
			});
			
			devId = 20000201;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки шрота 2"
			});

			devId = 20000202;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки шрота 3"
			});
			
			devId = 20000203;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки шрота 4"
			});
			
			devId = 20000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Розвантаження жиру"
			});
			
			context.SaveChanges();
			#endregion		
			
			#region Олія

			devId = 21000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Оператор погрузки олії"
			});
			
			devId = 21000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки олії 1"
			});
			
			devId = 21000201;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки олії 2"
			});
			
			devId = 21000202;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Точка погрузки олії 3"
			});
			
			devId = 21000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Погрузки олії."
			});
	
				devId = 21000301;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 21000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Погрузки олії. In 1"
				});
	
				devId = 21000302;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 21000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Погрузки олії. In 2"
				});
	
				devId = 21000303;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 21000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Погрузки олії. Out 1"
				});
	
				devId = 21000304;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 21000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Погрузки олії. Out 2"
				});
			
			context.SaveChanges();
			#endregion
			
			#region Склади

			devId = 22000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Склад"
			});
			
			devId = 22000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Митний склад"
			});
			
			context.SaveChanges();
			#endregion
			
			#region Склад тарировки

			devId = 23000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Склад тарировки (розвантаження)"
			});
			
			context.SaveChanges();
			#endregion
			
			#region МПЗ

			devId = 24000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Пульт МПЗ"
			});
			
			devId = 24000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Завантаження соєвої оболонки"
			});
			
			devId = 24000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Завантаження подрібненої лушпиння соняшника"
			});
			
			devId = 24000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Завантаження з підлогового складу Схеми №5"
			});
			
			context.SaveChanges();
			#endregion
			
			#region Дільниця №1 Візіровка
		
			devId = 25000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ідентифікатор автотранспорту. Візіровка №1"
			});
			
			devId = 25000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ідентифікатор персоналу. Візіровка №1"
			});
		
			context.SaveChanges();
			#endregion
			
			#region КПП №2 Пост №6 в'їзд / виїзд
			devId = 26000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Камера №1 виїзд"
			});

			devId = 26000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Камера №2 в'їзд"
			});

			devId = 26000201;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Охорона виїзд"
			});
			
			devId = 26000202;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Охорона виїзд"
			});
			context.SaveChanges();
			#endregion
			
			#region Авто вагова №5
			devId = 7000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.ScaleMettlerPT6S3,
				IsActive = true,
				Name = "Ваги №5"
			});

			devId = 7000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Ваги №5"
			});

			devId = 7000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Ваги 5."
			});

						devId = 7000401;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 5. In 1"
						});

						devId = 7000402;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 5. In 2"
						});

						devId = 7000403;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 5. In 3"
						});

						devId = 7000404;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000400,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 4in. Ваги 5. In 4"
						});

			devId = 7000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Ваги 5."
			});

						devId = 7000501;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 5. In 1"
						});

						devId = 7000502;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalIn,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 5. In 2"
						});

						devId = 7000503;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 5. Out 1"
						});

						devId = 7000504;
						context.Set<Device>().AddOrUpdate(new Device {
							Id = devId,
							ParentDeviceId = 7000500,
							ParamId = devId,
							StateId = devId,
							TypeId = Dom.Device.Type.DigitalOut,
							IsActive = true,
							Name = "VkModule 2in 2Out. Ваги 5. Out 2"
						});

			devId = 7000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 5. Камера 1"
			});
			
			devId = 7000601;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 5. Камера 2"
			});
			
			devId = 7000602;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.Camera,
				IsActive = true,
				Name = "Ваги 5. Камера 3"
			});

			context.SaveChanges();
			#endregion

			#region Точка вивантаження - Нижня територія
			devId = 27000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Вивантаження (Нижня територія)"
			});
			context.SaveChanges();
			#endregion

			#region Світлові індикатори

			devId = 8000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація Завантаження олійних №1"
			});

				devId = 8000101;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000102;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000103;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000104;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація Завантаження олійних №2"
			});

				devId = 8000201;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000202;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000203;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000204;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000300;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація схема №5 склад підлоговий"
			});

				devId = 8000301;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Valve 7"
				});

				devId = 8000302;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Valve 8"
				});

				devId = 8000303;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000304;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000300,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація дроблена лузга"
			});

				devId = 8000401;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000402;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000403;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000404;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000500;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація макуха"
			});

				devId = 8000501;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000500,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Valve 5"
				});

				devId = 8000502;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000500,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Valve 6"
				});

				devId = 8000503;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000500,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000504;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000500,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000600;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація Елеватор№1 зав Шроту"
			});

				devId = 8000601;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000600,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000602;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000600,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000603;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000600,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000604;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000600,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000700;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація Схема№5"
			});

				devId = 8000701;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000700,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000702;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000700,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000703;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000700,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000704;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000700,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000800;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація погрузка зернових"
			});

				devId = 8000801;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000800,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000802;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000800,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000803;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000800,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000804;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000800,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8000900;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд валідація вивантаження жиру"
			});

				devId = 8000901;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000900,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8000902;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000900,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8000903;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000900,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8000904;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8000900,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8001000;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд відбір проб шрота водій"
			});

				devId = 8001001;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001000,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8001002;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001000,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8001003;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001000,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8001004;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001000,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8001100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вїзд 10с12"
			});

				devId = 8001101;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8001102;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8001103;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8001104;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001100,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
				
			devId = 8001200;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule2In2Out,
				IsActive = true,
				Name = "VkModule 2in 2Out. Вивантаження комбікорму"
			});

				devId = 8001201;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 1"
				});

				devId = 8001202;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. In 2"
				});

				devId = 8001203;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 1"
				});

				devId = 8001204;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 8001200,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 2in 2Out. Вїзд. Out 2"
				});
	
			#endregion
			
			devId = 29000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Вузол ТМЦ"
			});
			
			devId = 22000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Valves"
			});

				devId = 22000401;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 22000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 1"
				});

				devId = 22000402;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 22000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 2"
				});

				devId = 22000403;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 22000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 3"
				});

				devId = 22000404;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 22000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 4"
				});
			
			devId = 23000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Valves"
			});

				devId = 23000401;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 23000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 9"
				});

				devId = 23000402;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 23000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 10"
				});

				devId = 23000403;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 23000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 11"
				});

				devId = 23000404;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 23000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 12"
				});
			
			devId = 26000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Valves"
			});

				devId = 26000401;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 26000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 13"
				});

				devId = 26000402;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 26000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 14"
				});

				devId = 26000403;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 26000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 15"
				});

				devId = 26000404;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 26000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 16"
				});
			
			devId = 25000400;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RelayVkmodule4In0Out,
				IsActive = true,
				Name = "VkModule 4in. Valves"
			});

				devId = 25000401;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 25000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 17"
				});

				devId = 25000402;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 25000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalIn,
					IsActive = true,
					Name = "VkModule 4in. Valve 18"
				});

				devId = 25000403;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 25000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 4in. -"
				});

				devId = 25000404;
				context.Set<Device>().AddOrUpdate(new Device {
					Id = devId,
					ParentDeviceId = 25000400,
					ParamId = devId,
					StateId = devId,
					TypeId = Dom.Device.Type.DigitalOut,
					IsActive = true,
					Name = "VkModule 4in. -"
				});
				
			devId = 36000100;
			context.Set<Device>().AddOrUpdate(new Device {
				Id = devId,
				ParentDeviceId = null,
				ParamId = devId,
				StateId = devId,
				TypeId = Dom.Device.Type.RfidObidRw,
				IsActive = true,
				Name = "RFID Obid. Лабораторія відвантаження 4"
			});

        }
	}
}
