using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DeviceMonitor.ViewModel.Report;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model;

namespace Gravitas.DeviceMonitor.Monitor
{
    internal class DeviceIpMonitor : BaseMonitor
    {
        private readonly IConnectManager _connectManager;
        private readonly INodeRepository _nodeRepository;
        private readonly ISettings _settings;
        private DateTime? _emailSendAt;

        public DeviceIpMonitor(INodeRepository nodeRepository,
            IConnectManager connectManager, 
            ISettings settings)
            : base(nodeRepository)
        {
            _nodeRepository = nodeRepository;
            _connectManager = connectManager;
            _settings = settings;
        }

        protected override void Process()
        {
            var node = _nodeRepository.GetEntity<Node, long>(NodeId);
            var deviceIps = GetDeviceIps(NodeId);
            var invalidIps = GetInvalidIps(deviceIps);

            if (invalidIps.Count != 0 && (!node.IsEmergency || (_emailSendAt.HasValue && (DateTime.Now - _emailSendAt.Value).Minutes > 60 * 3)))
            {
                var vm = new EmailVm
                {
                    NodeName = node.Name, Ips = invalidIps
                };
               
                _connectManager.SendEmail(Dom.Email.Template.DeviceInvalidInformation, _settings.AdminEmail, vm);
                _emailSendAt = DateTime.Now;
            }
            
            if (invalidIps.Count != 0 && !node.IsEmergency)
            {
                node.IsEmergency = true;
                _nodeRepository.Update<Node, long>(node);
            }
            
            if (invalidIps.Count == 0 && node.IsEmergency)
            {
                _emailSendAt = null;
                node.IsEmergency = false;
                _nodeRepository.Update<Node, long>(node);
            }
        }

        private IEnumerable<string> GetDeviceIps(long nodeId)
        {
            var ips = new List<string>();

            var node = _nodeRepository.GetNodeDto(nodeId);
            ips.AddRange(GetRfidIps(node.Config.Rfid));
            ips.AddRange(GetScaleIps(node.Config.Scale));
            ips.AddRange(GetCameraIps(node.Config.Camera));
            ips.AddRange(GetDiIps(node.Config.DI));
            ips.AddRange(GetDoIps(node.Config.DO));
            return ips.Distinct();
        }
    }
}