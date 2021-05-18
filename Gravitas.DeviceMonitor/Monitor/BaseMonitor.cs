using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Node;
using Gravitas.DeviceMonitor.ViewModel.Ip;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.Dto;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DeviceMonitor.Monitor
{
    public abstract class BaseMonitor
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly INodeRepository _nodeRepository;
        protected long NodeId;

        protected BaseMonitor(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public void ProcessLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Process();
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }

                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }

        public void Config(long nodeId)
        {
            NodeId = nodeId;
        }

        protected abstract void Process();

        protected List<InvalidIp> GetInvalidIps(IEnumerable<string> deviceIps)
        {
            var invalidIps = new List<InvalidIp>();
            var lastProcessed = "";
            try
            {
                foreach (var ip in deviceIps)
                {
                    lastProcessed = ip;
                    using (var ping = new Ping())
                    {
                        var ipArray = ip.Split('.').Select(x => Convert.ToByte(x)).ToArray();
                        if (ipArray.Length != 4) continue;

                        PingReply reply = null;
                        for (var i = GlobalConfigurationManager.DeviceMonitorDelay; i >= 0; i--)
                        {
                            reply = ping.Send(new IPAddress(new[]
                            {
                                ipArray[0], ipArray[1], ipArray[2], ipArray[3]
                            }));
                            if (reply?.Status == IPStatus.Success)
                            {
                                _logger.Debug($"Ping with {ip} is success");
                                break;
                            }

                            Thread.Sleep(1000);
                        }
                        
                        if (reply?.Status != IPStatus.Success)
                        {
                            _logger.Warn($"Ping with {ip} failed");
                            invalidIps.Add(new InvalidIp
                            {
                                Ip = ip
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Ping error: ip = {lastProcessed} error = {e.Message}");
                invalidIps.Add(new InvalidIp
                {
                    Ip = lastProcessed
                });
            }

            return invalidIps;
        }

        protected IEnumerable<string> GetScaleIps(Dictionary<string, NodeConfig.ScaleConfig> configScale)
        {
            var result = new List<string>();
            foreach (var device in configScale)
            {
                var d = _nodeRepository.GetEntity<Device, long>(device.Value.DeviceId);
                if (d.IsActive)
                {
                    var deviceParams = _nodeRepository.GetEntity<DeviceParam, long>(device.Value.DeviceId);
                    var match = Regex.Match(deviceParams.ParamJson, @"\d+\.\d+\.\d+\.\d+");
                    result.Add(match.Value);
                }
            }

            return result;
        }

        protected IEnumerable<string> GetRfidIps(Dictionary<string, NodeConfig.RfidConfig> configRfid)
        {
            var result = new List<string>();
            foreach (var device in configRfid)
            {
                DeviceParam deviceParams;
                switch (device.Key)
                {
                    case Dom.Node.Config.Rfid.LongRangeReader:
                        var parent = _nodeRepository.GetEntity<Device, long>(device.Value.DeviceId)?.ParentDeviceId;
                        if (!parent.HasValue) continue;
                        var p = _nodeRepository.GetEntity<Device, long>(parent.Value);
                        if (!p.IsActive)
                        {
                            continue;
                        }

                        deviceParams = _nodeRepository.GetEntity<DeviceParam, long>(parent.Value);
                        break;
                    case Dom.Node.Config.Rfid.TableReader:
                        var d = _nodeRepository.GetEntity<Device, long>(device.Value.DeviceId);
                        if (!d.IsActive)
                        {
                            continue;
                        }

                        deviceParams = _nodeRepository.GetEntity<DeviceParam, long>(device.Value.DeviceId);
                        break;
                    default:
                        return result;
                }

                var match = Regex.Match(deviceParams.ParamJson, @"\d+\.\d+\.\d+\.\d+");
                result.Add(match.Value);
            }

            return result;
        }

        protected IEnumerable<string> GetDoIps(Dictionary<string, NodeConfig.DoConfig> configDo)
        {
            var result = new List<string>();
            foreach (var device in configDo)
            {
                var parent = _nodeRepository.GetEntity<Device, long>(device.Value.DeviceId)?.ParentDeviceId;
                if (!parent.HasValue) continue;
                var p = _nodeRepository.GetEntity<Device, long>(parent.Value);
                if (p.IsActive)
                {
                    var deviceParams = _nodeRepository.GetEntity<DeviceParam, long>(parent.Value);
                    var match = Regex.Match(deviceParams.ParamJson, @"\d+\.\d+\.\d+\.\d+");
                    result.Add(match.Value);
                }
            }

            return result;
        }

        protected IEnumerable<string> GetDiIps(Dictionary<string, NodeConfig.DiConfig> configDi)
        {
            var result = new List<string>();
            foreach (var device in configDi)
            {
                var parent = _nodeRepository.GetEntity<Device, long>(device.Value.DeviceId)?.ParentDeviceId;
                if (!parent.HasValue) continue;
                var p = _nodeRepository.GetEntity<Device, long>(parent.Value);
                if (p.IsActive)
                {
                    var deviceParams = _nodeRepository.GetEntity<DeviceParam, long>(parent.Value);
                    var match = Regex.Match(deviceParams.ParamJson, @"\d+\.\d+\.\d+\.\d+");
                    result.Add(match.Value);
                }
            }

            return result;
        }

        protected IEnumerable<string> GetCameraIps(Dictionary<string, NodeConfig.CameraConfig> configCamera)
        {
            var result = new List<string>();
            foreach (var device in configCamera)
            {
                var d = _nodeRepository.GetEntity<Device, long>(device.Value.DeviceId);
                if (!d.IsActive)
                {
                    continue;
                }

                var deviceParams = _nodeRepository.GetEntity<DeviceParam, long>(device.Value.DeviceId);
                var match = Regex.Match(deviceParams.ParamJson, @"\d+\.\d+\.\d+\.\d+");
                result.Add(match.Value);
            }

            return result;
        }
    }
}