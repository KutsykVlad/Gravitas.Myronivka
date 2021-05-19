using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.OpData.NonStandart
{
    public class NodeInfo
    {
        [DisplayName("Ідентифікатор вузла")] 
        public int? NodeId { get; set; }
        [DisplayName("Назва вузла")] 
        public string NodeName { get; set; }
    }
}