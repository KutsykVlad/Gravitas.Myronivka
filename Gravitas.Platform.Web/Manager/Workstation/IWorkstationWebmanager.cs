using Gravitas.Platform.Web.ViewModel.Workstation;

namespace Gravitas.Platform.Web.Manager.Workstation
{
    public interface IWorkstationWebManager
    {
        WorkstationStateVm GetWorkstationNodes(int id);
    }
}