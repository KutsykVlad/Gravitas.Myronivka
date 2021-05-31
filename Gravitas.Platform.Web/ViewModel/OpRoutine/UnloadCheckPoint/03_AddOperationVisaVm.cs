using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class UnloadCheckPointVms
    {
        public class AddOperationVisaVm
        {
            public int NodeId { get; set; }

            public string Rfid { get; set; }
            public DateTime ReadTime { get; set; }
        }
    }
}