using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class UnloadPointType2Vms
    {
        public class AddOperationVisaVm
        {
            public long NodeId { get; set; }
            public string Rfid { get; set; }
            public DateTime ReadTime { get; set; }
        }
    }
}