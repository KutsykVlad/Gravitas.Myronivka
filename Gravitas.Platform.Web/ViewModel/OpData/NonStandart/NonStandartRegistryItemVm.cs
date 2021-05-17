using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Gravitas.Platform.Web.ViewModel.NonStandard;

namespace Gravitas.Platform.Web.ViewModel.OpData.NonStandart {
    public class NonStandartRegistryItemVm {
        public Guid Id { get; set; }
        [DisplayName("Тип операції")]
        public object OpDataType { get; set; }

        public long StateId { get; set; }
        [DisplayName("Cтатус")]
        public string StateName { get; set; }
        [DisplayName("Дата\\Час заїзду")]
        public DateTime? CheckInDateTime { get; set; }
        [DisplayName("Дата\\Час від’їзду")]
        public DateTime? CheckOutDateTime { get; set; }

        public long? NodeId { get; set; }

        [DisplayName("Контрольна точка")]
        public string NodeName { get; set; }
        
        [DisplayName("Повідомлення")]
        public string Message { get; set; }

        public TruckBaseData TruckBaseData { get; set; }
    }
}