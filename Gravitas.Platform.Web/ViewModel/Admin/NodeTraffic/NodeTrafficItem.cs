using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class NodeTrafficItem
    {
        public long NodeId { get; set; }
        [DisplayName("Назва вузла")]
        public string NodeName { get; set; }
        [DisplayName("Ідентифікатор контейнера")]
        public long TicketContainerId { get; set; }
        [DisplayName("Час в'їзду")]
        public DateTime? EntranceTime { get; set; }
        [DisplayName("Час від'їзду")]
        public DateTime? DepartureTime { get; set; }
    }
}