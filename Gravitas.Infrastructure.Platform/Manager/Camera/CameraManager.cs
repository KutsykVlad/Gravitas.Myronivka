using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceParam;
using Gravitas.Model.Dto;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public class CameraManager : ICameraManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IDeviceRepository _deviceRepository;
        private readonly GravitasDbContext _context;
        private readonly INodeRepository _nodeRepository;


        public CameraManager(
            INodeRepository nodeRepository,
            IDeviceRepository deviceRepository, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _deviceRepository = deviceRepository;
            _context = context;
        }

        public bool TakeSnapshot(string host, string user, string pass, string path)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(host);
                request.Credentials = new NetworkCredential(user, pass);
                request.Timeout = 10000;

                using (WebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream == null) return false;
                    using (Stream fileStream = File.Create(path))
                    {
                        responseStream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }

            return true;
        }

        public bool TakeSnapshot(CameraParam param, string filename)
        {
            if (param == null)
                return false;

            return TakeSnapshot(param.IpAddress, param.Login, param.Password, filename);
        }

        public List<long> GetSnapshots(NodeConfig nodeConfig)
        {
            var cameraImagesList = new List<long>();

            if (nodeConfig == null) return null;

            foreach (var cameraDeviceId in nodeConfig.Camera.Values.Select(e => e.DeviceId))
            {
                var device = _context.Devices.First(x => x.Id ==cameraDeviceId);

                if (device.ParamId == null)
                {
                    Logger.Warn($"Device {device.Id} has ParamId == null");
                    continue;
                }

                var cameraParam =
                    CameraParam.FromJson(_context.DeviceParams.First(x => x.Id == device.ParamId.Value).ParamJson);

                var filename = GenerateCamImagePath(
                    GlobalConfigurationManager.CameraImageBasePath,
                    $"{GlobalConfigurationManager.CameraImageNamePrefix}DevId-{cameraDeviceId}");

                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                    continue;
                TakeSnapshot(cameraParam, filename);

                var opCameraImage = new OpCameraImage
                {
                    DateTime = DateTime.Now, SourceDeviceId = device.Id, ImagePath = filename
                };
                _nodeRepository.Add<OpCameraImage, long>(opCameraImage);

                cameraImagesList.Add(opCameraImage.Id);

                // todo: delete console test
//                Logger.Debug($"CAMERA {device.Id} finished");
            }

            return cameraImagesList;
        }

        private string GenerateCamImagePath(string dir, string prefix)
        {
            var now = DateTime.Now;
            dir = Path.Combine(dir, $"{now.Year:0000}", $"{now.Month:00}", $"{now.Day:00}");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(
                dir,
                $"{prefix}-{now.Year:0000}{now.Month:00}{now.Day:00}-{now.Hour:00}{now.Minute:00}{now.Second:00}{now.Millisecond:000}.jpeg");
            return path;
        }
    }
}