using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.Platform.Web.ViewModel.Helper
{
    public class GetTareValue
    {
        public long NodeId { get; set; }
        public TicketPackingTareVm[] TareItems { get; set; }
        public int OpRoutineReturnState { get; set; }
        public long ReturnRoutineStateId { get; set; }
    }
}