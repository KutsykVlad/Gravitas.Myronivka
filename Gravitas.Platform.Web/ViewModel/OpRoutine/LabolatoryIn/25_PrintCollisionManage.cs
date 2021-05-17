using System;
using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Model;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.Platform.Web.ViewModel {

    public static partial class LaboratoryInVms {

        public class PrintCollisionManageVm {
            public long NodeId { get; set; }
            public Guid OpDataId { get; set; }
            
            [DisplayName("Стан")]
            public string OpDataState { get; set; }

            public SamplePrintoutVm SamplePrintoutVm { get; set; }
            
            [DisplayName("Причина повернення")]
            public string ReasonsForRefundId { get; set; }
            public List<ExternalData.ReasonForRefund> ReasonsForRefund { get; set; }
            public bool IsLabFile { get; set; }
        }
    }
}