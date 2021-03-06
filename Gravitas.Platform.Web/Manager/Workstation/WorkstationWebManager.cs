using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Platform.Web.ViewModel.Workstation;

namespace Gravitas.Platform.Web.Manager.Workstation
{
    public class WorkstationWebManager : IWorkstationWebManager
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IExternalDataRepository _externalDataRepository;

        public WorkstationWebManager(INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            IExternalDataRepository externalDataRepository)
        {
            _nodeRepository = nodeRepository;
            _opDataRepository = opDataRepository;
            _externalDataRepository = externalDataRepository;
        }

        public WorkstationStateVm GetWorkstationNodes(int id)
        {
            // var workstationNodes = _organizationUnitRepository.GetOrganizationUnitDetail(id);
            // var vm = new WorkstationStateVm
            // {
            //     Name = workstationNodes.Name,
            //     Id = id,
            //     Items = workstationNodes.NodeItems.Items
            //                             .Select(item =>
            //                             {
            //                                 var nodeContext = _nodeRepository.GetNodeContext(item.Id);
            //                                 
            //                                 var  model = new WorkstationStateItemVm
            //                                 {
            //                                     NodeId = item.Id,
            //                                     NodeName = item.Name,
            //                                     StateId = nodeContext.OpRoutineStateId,
            //                                     NodeState = GetNodeState(item.Id),
            //                                     StateName = string.Empty,
            //                                     IsCleanupMode = _nodeRepository.GetNodeContext(item.Id).OpProcessData != null
            //                                 };
            //
            //                                 var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeContext.TicketId);
            //                                 if (singleWindowOpData != null)
            //                                     if (singleWindowOpData.IsThirdPartyCarrier)
            //                                     {
            //                                         model.TransportNo = singleWindowOpData.HiredTransportNumber;
            //                                         model.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            //                                     }
            //                                     else
            //                                     {
            //                                         model.TransportNo =
            //                                             _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ??
            //                                             string.Empty;
            //                                         model.TrailerNo =
            //                                             _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ??
            //                                             string.Empty;
            //                                     }
            //                                 return model;
            //                             })
            // };

            return null;
        }

        private long GetNodeState(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return -1;

            switch (nodeDto.IsActive)
            {
                case false:
                    return NodeData.ActiveState.NotActive;
                default:
                    return nodeDto.Context.TicketContainerId.HasValue
                        ? NodeData.ActiveState.InWork
                        : NodeData.ActiveState.Active;
            }
        }
    }
}