using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.DAL.PostDeployment;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLaboratoryProcess
    {
        public class PrintDataDiscloseVm
        {
            public long NodeId { get; set; }
            public bool IsLabFile { get; set; }
            public bool IsCollisionMode { get; set; }
            public string CollisionApprovalMessage { get; set; }

            public CentralLabVms.CentralLabOpDataOpDataDetailVm OpDataDetail { get; set; }
        }
    }
}