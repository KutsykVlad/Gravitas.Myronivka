using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;

namespace Gravitas.Platform.Web.Manager
{
    public interface ITicketContainerWebManager
    {
        IEnumerable<long> GetActiveTicketContainers(List<long> inputNodeIds);
        ICollection<UnloadGuideTicketContainerItemVm> GetUnloadGuideTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<LabFacelessTicketContainerItemVm> GetLabFacelessTicketContainerItemsVm();
        ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<UnloadPointTicketContainerItemVm> GetUnloadPointTicketContainerItemsVm(IEnumerable<long> containerIds, long nodeId);
        List<LoadGuideTicketContainerItemVm> GetLoadGuideTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<LoadPointTicketContainerItemVm> GetLoadPointTicketContainerItemsVm(IEnumerable<long> containerIds, long nodeId);
        ICollection<LoadGuideTicketContainerItemVm> GetRejectedLoadGuideTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<UnloadGuideTicketContainerItemVm> GetRejectedUnloadGuideTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<MixedFeedGuideTicketContainerItemVm> GetMixedFeedGuideTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<MixedFeedLoadTicketContainerItemVm> GetMixedFeedLoadTicketContainerItemsVm(IEnumerable<long> containerIds, long nodeId);
        ICollection<MixedFeedGuideTicketContainerItemVm> GetRejectedMixedFeedLoadTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<MixedFeedUnloadTicketContainerItemVm> GetRejectedMixedFeedUnloadTicketContainerItemsVm(IEnumerable<long> containerIds);
        ICollection<CentralLabTicketContainerItemVm> GetCentralLabTicketContainerListVm(IEnumerable<long> containerIds);
        ICollection<LabFacelessTicketContainerItemVm> GetSelfServiceLabTicketContainerItemsVm();
        ICollection<UnloadQueueTicketContainerItemVm> GetUnloadQueueTicketContainerItemsVm();
    }
}