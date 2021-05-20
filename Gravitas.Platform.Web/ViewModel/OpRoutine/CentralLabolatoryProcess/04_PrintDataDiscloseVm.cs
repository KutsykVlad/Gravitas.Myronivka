namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLaboratoryProcess
    {
        public class PrintDataDiscloseVm
        {
            public int NodeId { get; set; }
            public bool IsLabFile { get; set; }
            public bool IsCollisionMode { get; set; }
            public string CollisionApprovalMessage { get; set; }

            public CentralLabVms.CentralLabOpDataOpDataDetailVm OpDataDetail { get; set; }
        }
    }
}