using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DAO;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LaboratoryInVms
    {
        public class PrintDataDiscloseVm
        {
            public long NodeId { get; set; }
            public Guid OpDataId { get; set; }
            public bool IsLabFile { get; set; }

            public SamplePrintoutVm SamplePrintoutVm { get; set; }

            public string ReasonsForRefundId { get; set; }
            public List<ReasonForRefund> ReasonsForRefund { get; set; }
            public string DocumentTypeId { get; set; }
        }
    }
}