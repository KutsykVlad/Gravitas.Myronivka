using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class MixedFeedLoadVms
    {
        public class CleanupVm
        {
            public long NodeId { get; set; }
            
            [DisplayName("Час очистки (хв)")]
            public int CleanupTime { get; set; }
        }
    }
}