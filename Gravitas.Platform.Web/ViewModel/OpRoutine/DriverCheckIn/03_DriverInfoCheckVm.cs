using System;

namespace Gravitas.Platform.Web.ViewModel.OpRoutine.DriverCheckIn
{
    public static partial class DriverCheckInVms
    {
        public class DriverInfoCheckVm
        {
            public int NodeId { get; set; }

            public string Rfid { get; set; }
            public DateTime ReadTime { get; set; }
        }
    }
}