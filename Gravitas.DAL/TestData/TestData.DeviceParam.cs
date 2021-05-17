using System.Data.Entity.Migrations;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.Dto;

namespace Gravitas.DAL.TestData {

	public static partial class TestData {

		public static void DeviceParam(GravitasDbContext context)
		{
			#region Test Devices

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "192.168.0.180",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 2,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "192.168.0.182",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 12,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.10/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "192.168.0.170",
			//		Port = 1749
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 15,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.72",
			//		Port = 1749
			//	}.ToJson()
			//});
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 16,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.76",
			//		Port = 1749
			//	}.ToJson()
			//});
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.81",
			//		Port = 1749
			//	}.ToJson()
			//});
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 18,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.87",
			//		Port = 1749
			//	}.ToJson()
			//});
			//// -------------
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "192.168.0.181",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 21,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 22,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 23,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 3
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 24,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 4
			//	}.ToJson()
			//});

			#endregion

			#region 02.01.01  КПП №1 верхняя территория

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000100,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.10/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000200,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.11/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.12",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000400,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.13",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000500,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.14",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000501,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000600,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.16",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000700,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.18",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000701,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000702,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000703,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000704,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000800,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.19",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000801,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000802,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000803,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000804,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000900,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.7",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000901,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000902,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000903,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1000904,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1001000,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.15",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1001001,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1001002,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1001003,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 1001004,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			#endregion

			//#region 04.01.01  Авто №1 верхняя территория 

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000100,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.68",
			//		Port = 1749
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.70",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.72",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000401,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000402,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000403,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 3
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000404,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 4
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.73",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000501,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000502,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000503,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000504,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000600,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.69/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000700,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.123/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 3000800,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.124/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//#endregion

			//#region 04.01.02  Авто №2 верхняя территория 

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000100,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.75",
			//		Port = 1749
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.76",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.78",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000401,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000402,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000403,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 3
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000404,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 4
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.79",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000501,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000502,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000503,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000504,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000600,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.74/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000700,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.125/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 4000800,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.126/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//#endregion

			//#region 04.01.03  Авто №3 верхняя территория 

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000100,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.81",
			//		Port = 1749
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.83",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.85",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000401,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000402,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000403,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 3
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000404,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 4
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.86",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000501,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000502,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000503,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000504,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000600,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.82/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000700,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.141/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 5000800,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.142/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//#endregion

			//#region 04.01.04  Авто №4 верхняя территория 

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000100,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.87",
			//		Port = 1749
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.89",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.91",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000401,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000402,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000403,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 3
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000404,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 4
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.92",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000501,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000502,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000503,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000504,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000600,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.88/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000700,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.143/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 6000800,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.41.145/jpg/image.jpg",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "admin"
			//	}.ToJson()
			//});


			//#endregion

			//#region Візіровка №0

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 7000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.48",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion
			
			//#region 03.01.01  Візіровка №3 верхняя территория 

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 800300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.47",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 800500,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.45",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 800501,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});


			//#endregion

			//#region 03.01.0X  Візіровка - Лабораторія 

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 9000100,
			//	ParamJson = new LabFossParam()
			//	{
			//		Directory = @"D:\FTP\GravitasLabs\01002_KKZ_Foss"
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 9000101,
			//	ParamJson = new LabFossParam()
			//	{
			//		Directory = @"D:\FTP\GravitasLabs\01003_KKZ_Foss_New"
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 9000200,
			//	ParamJson = new LabBrukerParam()
			//	{
			//		Directory = @"D:\FTP\GravitasLabs\01001_KKZ_Bruker"
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 9000300,
			//	ParamJson = new LabBrukerParam()
			//	{
			//		Directory = @"D:\FTP\GravitasLabs\Infraskan"
			//	}.ToJson()
			//});

			//#endregion
			
			//#region 03.01.02  Візіровка №4 верхняя территория 
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam {
			//	Id = 9000400,
			//	ParamJson = new RfidObidRwParam {
			//		IpAddress = "10.64.75.46",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam {
			//	Id = 9000500,
			//	ParamJson = new RfidZebraFx9500HeadParam() {
			//		IpAddress = "10.64.75.44",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam {
			//	Id = 9000501,
			//	ParamJson = new RfidZebraFx9500AntennaParam {
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});
			//#endregion

			//#region 01.01.01  Єдине вікно  

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 10000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.2",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 10000400,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.3",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 10000500,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.5",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion

			//#region 03.05.04  Елеватор№4 та Елеватор№5

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.149",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.150",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000201,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.215",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000202,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.216",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000300,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.151",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000301,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000400,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.152",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000401,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000500,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.153",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000501,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000600,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.156",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000601,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000602,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000603,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000604,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000700,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.155",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000701,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000702,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000703,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000704,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000800,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.154",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000801,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000802,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000803,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 11000804,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//#endregion

			//#region 03.05.04  Елеватор№3

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.128",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000700,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.141",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000500,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.131",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000600,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.132",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000200,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.130",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000201,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000300,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.129",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000301,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});


			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.133",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000401,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000402,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000403,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000404,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//#endregion

			//#region Призначення ями вигрузки 1

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 12000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.147",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion
			
			//#region Призначення ями вигрузки 2

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 12000400,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.235",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion

			//#region Черга

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 13000001,
			//	ParamJson = new DisplayParam
			//	{
			//		IpAddress = "10.64.75.230",
			//		IpPort = 1234
			//	}.ToJson()
			//});

		 //   context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
		 //   {
		 //       Id = 13000002,
		 //       ParamJson = new DisplayParam
		 //       {
		 //           IpAddress = "10.64.75.231",
		 //           IpPort = 1234
		 //       }.ToJson()
		 //   });
		    
		 //   context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
		 //   {
		 //       Id = 13000003,
		 //       ParamJson = new DisplayParam
		 //       {
		 //           IpAddress = "10.64.75.232",
		 //           IpPort = 1234
		 //       }.ToJson()
		 //   });

   //         #endregion

			//#region 03.05.04  Елеватор№2

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.137",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000200,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.138",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000201,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000300,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.139",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000301,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000400,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.140",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000401,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.142",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000501,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000502,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000503,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000504,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000600,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.143",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000601,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000602,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000603,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 1
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 14000604,
			//	ParamJson = new DigitalInOutParam()
			//	{
			//		No = 2
			//	}.ToJson()
			//});

			//#endregion
			
			//#region Центральна лабораторія

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 15000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.105",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 15000200,
			//	ParamJson = new RfidObidRwParam()
			//	{
			//		IpAddress = "10.64.75.104",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 16000100,
			//	ParamJson = new RfidObidRwParam()
			//	{
			//		IpAddress = "10.64.75.95",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 16000200,
			//	ParamJson = new RfidObidRwParam()
			//	{
			//		IpAddress = "10.64.75.96",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 16000300,
			//	ParamJson = new RfidObidRwParam()
			//	{
			//		IpAddress = "10.64.75.97",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion
			
			//#region Комбікормовий завод

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.190",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.194",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.193",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.191",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100400,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.192",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100900,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.205",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100110,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.195",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100111,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100112,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100113,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
		
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100114,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100120,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.196",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100121,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100122,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100123,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
		
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100124,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100130,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.197",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100131,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
		
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100132,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100133,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100134,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100140,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.198",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100141,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100142,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
					
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100143,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
				
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100144,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100150,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.199",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100151,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100152,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
					
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100153,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
				
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100154,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100160,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.200",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100161,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100162,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
					
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100163,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
				
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100164,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100170,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.201",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100171,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100172,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
					
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100173,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
				
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100174,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100180,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.202",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100181,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
			
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100182,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
					
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100183,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
				
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100184,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100210,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.203",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100211,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100212,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100213,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100214,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});	
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17100220,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.204",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100221,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100222,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100223,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17100224,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});			
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17200400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.209",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200401,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200402,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200403,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200404,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17200300,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.206",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200301,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200302,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200303,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200304,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});	
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17200200,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.207",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200201,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200202,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200203,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200204,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 17200100,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.208",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200101,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200102,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200103,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 17200104,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
			
			
			//#endregion
			
			//#region Елеватор 1

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 19000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.119",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 19000200,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.120",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 19000201,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 19000210,
			//	ParamJson = new RfidZebraFx9500HeadParam()
			//	{
			//		IpAddress = "10.64.75.121",
			//		IpPort = 0,
			//		Timeout = 0
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 19000211,
			//	ParamJson = new RfidZebraFx9500AntennaParam
			//	{
			//		AntennaId = 1,
			//		PeakRssiLimit = 0,
			//		TagActiveSeconds = 0
			//	}.ToJson()
			//});
	
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 19000300,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.122",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 19000301,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 19000302,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 19000304,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 19000303,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//#endregion
			
			//#region Шрот

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.113",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.117",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20000201,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.116",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20000202,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.103",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20000203,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.115",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 20000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.114",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion
			
			//#region Олія

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 21000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.238",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 21000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.169",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 21000201,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.240",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 21000202,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.239",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 21000300,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.159",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 21000301,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
		
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 21000302,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
		
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 21000303,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
		
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 21000304,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
			
			//#endregion
			
			//#region Склади

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 22000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.125",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 22000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.171",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 23000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.124",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 27000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.63",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//#endregion
			
			//#region МПЗ
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 24000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.214",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 24000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.158",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 24000300,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.217",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 24000400,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.157",
			//		Port = 9761
			//	}.ToJson()
			//});
			//#endregion
			
			//#region Дільниця №1 Візіровка

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 25000100,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.60",
			//		Port = 9761
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 25000200,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.61",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion
			
			//#region КПП №2 Пост №6 в'їзд / виїзд
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 26000100,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.20/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 26000200,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.21/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});
			
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 26000202,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.24",
			//		Port = 9761
			//	}.ToJson()
			//});

			//#endregion
			
			#region Авто вагова №5 
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 7000100,
			//	ParamJson = new ScaleMettlerPT6S3Param()
			//	{
			//		IpAddress = "10.64.75.41",
			//		Port = 1749
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 7000700,
			//	ParamJson = new RfidObidRwParam
			//	{
			//		IpAddress = "10.64.75.37",
			//		Port = 9761
			//	}.ToJson()
			//});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 7000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.39",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000

			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000401,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000402,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000403,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 3
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000404,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 4
			//		}.ToJson()
			//	});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 7000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.40",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000501,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000502,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000503,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});
	
			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 7000504,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 7000600,
			//	ParamJson = new CameraParam
			//	{
			//		IpAddress = "http://10.64.75.35/ISAPI/Streaming/channels/101/picture?snapShotImageType=JPEG",
			//		Port = 554,
			//		Login = "admin",
			//		Password = "Qq19832004"
			//	}.ToJson()
			//});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 7000601,
				ParamJson = new CameraParam
				{
					IpAddress = "http://10.64.41.161/jpg/image.jpg",
					Port = 554,
					Login = "admin",
					Password = "admin"
				}.ToJson()
			});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 7000602,
				ParamJson = new CameraParam
				{
					IpAddress = "http://10.64.41.162/jpg/image.jpg",
					Port = 554,
					Login = "admin",
					Password = "admin"
				}.ToJson()
			});

			#endregion

			//#region Світлові індикатори

			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000100,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.223",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000101,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000102,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000103,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000104,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000200,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.222",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000201,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000202,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000203,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000204,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000300,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.218",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000301,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000302,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000303,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000304,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000400,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.221",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000401,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000402,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000403,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000404,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000500,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.219",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000501,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000502,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000503,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000504,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000600,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.107",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000601,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000602,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000603,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000604,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000700,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.108",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000701,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000702,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000703,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000704,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000800,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.109",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000801,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000802,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000803,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000804,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8000900,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.110",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000901,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000902,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000903,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8000904,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8001000,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.101",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001001,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001002,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001003,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001004,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8001100,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.102",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001101,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001102,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001103,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001104,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});
				
			//context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//{
			//	Id = 8001200,
			//	ParamJson = new VkModuleSocketXParam
			//	{
			//		IpAddress = "10.64.75.126",
			//		Login = "admin",
			//		Password = "vkmodule",
			//		Timeout = 5000
			//	}.ToJson()
			//});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001201,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001202,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001203,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 1
			//		}.ToJson()
			//	});

			//	context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			//	{
			//		Id = 8001204,
			//		ParamJson = new DigitalInOutParam()
			//		{
			//			No = 2
			//		}.ToJson()
			//	});

			//#endregion
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 29000100,
				ParamJson = new RfidObidRwParam
				{
					IpAddress = "10.64.75.53",
					Port = 9761
				}.ToJson()
			});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 22000400,
				ParamJson = new VkModuleSocketXParam
				{
					IpAddress = "10.64.75.220",
					Login = "admin",
					Password = "vkmodule",
					Timeout = 5000
				}.ToJson()
			});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 23000400,
				ParamJson = new VkModuleSocketXParam
				{
					IpAddress = "10.64.75.222",
					Login = "admin",
					Password = "vkmodule",
					Timeout = 5000
				}.ToJson()
			});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 26000400,
				ParamJson = new VkModuleSocketXParam
				{
					IpAddress = "10.64.75.221",
					Login = "admin",
					Password = "vkmodule",
					Timeout = 5000
				}.ToJson()
			});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 25000400,
				ParamJson = new VkModuleSocketXParam
				{
					IpAddress = "10.64.75.226",
					Login = "admin",
					Password = "vkmodule",
					Timeout = 5000
				}.ToJson()
			});
			
			context.Set<DeviceParam>().AddOrUpdate(new DeviceParam
			{
				Id = 36000100,
				ParamJson = new RfidObidRwParam
				{
					IpAddress = "10.64.75.227",
					Port = 9761
				}.ToJson()
			});
			
			context.SaveChanges();
		}
	}
}
