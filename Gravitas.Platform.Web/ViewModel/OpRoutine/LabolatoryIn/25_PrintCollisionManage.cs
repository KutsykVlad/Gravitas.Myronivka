using System;
using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DAO;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LaboratoryInVms
    {
        public class PrintCollisionManageVm
        {
            public int NodeId { get; set; }
            public Guid OpDataId { get; set; }

            [DisplayName("Стан")] public string OpDataState { get; set; }

            public SamplePrintoutVm SamplePrintoutVm { get; set; }

            [DisplayName("Причина повернення")] public string ReasonsForRefundId { get; set; }
            public List<ReasonForRefund> ReasonsForRefund { get; set; }
            public bool IsLabFile { get; set; }
        }
    }
}