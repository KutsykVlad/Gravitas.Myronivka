using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.Platform.Web.ViewModel.Helper
{
    public class GetTareValue
    {
        public int NodeId { get; set; }
        public TicketPackingTareVm[] TareItems { get; set; }
        public int OpRoutineReturnState { get; set; }
        public int ReturnRoutineStateId { get; set; }
    }
}