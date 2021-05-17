using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SecurityOutVms
    {
        public class EditStampListVm
        {
            public long NodeId { get; set; }
            
            [DisplayName("Список пломб")] 
            public string StampList { get; set; }

            public bool IsTechRoute { get; set; }
        }
    }
}