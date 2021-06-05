using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.OpRoutine.DriverCheckIn
{
    public static partial class DriverCheckInVms
    {
        public class DriverInfoCheckVm
        {
            public int NodeId { get; set; }
            
            [DisplayName("Номер телефону")]
            public string PhoneNumber { get; set; }
            
            [DisplayName("ПІП водія")]
            public string Driver { get; set; }
            
            [DisplayName("Номер авто")]
            public string Truck { get; set; }
            
            [DisplayName("Номер причіпа")]
            public string Trailer { get; set; }
            public string PhotoUrl { get; set; }
        }
    }
}