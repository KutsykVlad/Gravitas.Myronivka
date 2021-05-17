using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gravitas.Platform.Web.ViewModel.OpData.NonStandart {
    public class NodeInfo {
        [DisplayName("Ідентифікатор вузла")]
        public long? NodeId { get; set; }
        [DisplayName("Назва вузла")]
        public string NodeName { get; set; }
    }
}