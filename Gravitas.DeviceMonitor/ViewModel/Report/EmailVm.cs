using System.Collections.Generic;
using Gravitas.DeviceMonitor.ViewModel.Ip;

namespace Gravitas.DeviceMonitor.ViewModel.Report
{
    public class EmailVm
    {
        public string NodeName { get; set; }
        public List<InvalidIp> Ips { get; set; }
    }
}