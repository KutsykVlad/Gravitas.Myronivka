namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class EditTicketFormVm
        {
            public int NodeId { get; set; }
            public int OpDataId { get; set; }

            public SingleWindowOpDataDetailVm SingleWindowOpDataDetailVm { get; set; }

            public bool IsEditable { get; set; }
        }
    }
}