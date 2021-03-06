using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;

namespace Gravitas.Platform.Web.Manager.TicketContainer
{
    public interface ITicketContainerWebManager
    {
        IEnumerable<int> GetActiveTicketContainers(List<int> inputNodeIds);
        ICollection<UnloadGuideTicketContainerItemVm> GetUnloadGuideTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<LabFacelessTicketContainerItemVm> GetLabFacelessTicketContainerItemsVm();
        ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowQueueTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowInProgressTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowProcessedTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<UnloadPointTicketContainerItemVm> GetUnloadPointTicketContainerItemsVm(IEnumerable<int> containerIds, int nodeId);
        List<LoadGuideTicketContainerItemVm> GetLoadGuideTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<LoadPointTicketContainerItemVm> GetLoadPointTicketContainerItemsVm(IEnumerable<int> containerIds, int nodeId);
        ICollection<LoadGuideTicketContainerItemVm> GetRejectedLoadGuideTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<UnloadGuideTicketContainerItemVm> GetRejectedUnloadGuideTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<MixedFeedGuideTicketContainerItemVm> GetMixedFeedGuideTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<MixedFeedLoadTicketContainerItemVm> GetMixedFeedLoadTicketContainerItemsVm(IEnumerable<int> containerIds, int nodeId);
        ICollection<MixedFeedGuideTicketContainerItemVm> GetRejectedMixedFeedLoadTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<MixedFeedUnloadTicketContainerItemVm> GetRejectedMixedFeedUnloadTicketContainerItemsVm(IEnumerable<int> containerIds);
        ICollection<CentralLabTicketContainerItemVm> GetCentralLabTicketContainerListVm(IEnumerable<int> containerIds);
        ICollection<LabFacelessTicketContainerItemVm> GetSelfServiceLabTicketContainerItemsVm();
        ICollection<UnloadQueueTicketContainerItemVm> GetUnloadQueueTicketContainerItemsVm();
    }
}