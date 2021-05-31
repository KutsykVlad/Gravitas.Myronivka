using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.DeviceMonitor.ViewModel.Report;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DeviceMonitor.Monitor
{
    internal class DeviceIpMonitor : BaseMonitor
    {
        private readonly IConnectManager _connectManager;
        private readonly INodeRepository _nodeRepository;
        private readonly ISettings _settings;
        private readonly GravitasDbContext _context;
        private DateTime? _emailSendAt;

        public DeviceIpMonitor(INodeRepository nodeRepository,
            IConnectManager connectManager, 
            ISettings settings, 
            GravitasDbContext context)
            : base(context)
        {
            _nodeRepository = nodeRepository;
            _connectManager = connectManager;
            _settings = settings;
            _context = context;
        }

        protected override void Process()
        {
            var node = _context.Nodes.First(x => x.Id == NodeId);
            var deviceIps = GetDeviceIps(NodeId);
            var invalidIps = GetInvalidIps(deviceIps);

            if (invalidIps.Count != 0 && (!node.IsEmergency || (_emailSendAt.HasValue && (DateTime.Now - _emailSendAt.Value).Minutes > 60 * 3)))
            {
                var vm = new EmailVm
                {
                    NodeName = node.Name, Ips = invalidIps
                };
               
                _connectManager.SendEmail(EmailTemplate.DeviceInvalidInformation, _settings.AdminEmail, vm);
                _emailSendAt = DateTime.Now;
            }
            
            if (invalidIps.Count != 0 && !node.IsEmergency)
            {
                node.IsEmergency = true;
                _context.SaveChanges();
            }
            
            if (invalidIps.Count == 0 && node.IsEmergency)
            {
                _emailSendAt = null;
                node.IsEmergency = false;
                _context.SaveChanges();

            }
        }

        private IEnumerable<string> GetDeviceIps(int nodeId)
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