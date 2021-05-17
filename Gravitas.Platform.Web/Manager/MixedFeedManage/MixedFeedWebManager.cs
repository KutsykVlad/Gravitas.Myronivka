using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using NLog;
using Node = Gravitas.Model.Node;

namespace Gravitas.Platform.Web.Manager.MixedFeedManage
{
    public class MixedFeedWebManage : IMixedFeedWebManager
    {
        private const long MixedFeedNodeId = (long) NodeIdValue.MixedFeedManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly GravitasDbContext _context;

        public MixedFeedWebManage(INodeRepository nodeRepository, 
            IOpRoutineManager opRoutineManager, 
            IExternalDataRepository externalDataRepository, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _opRoutineManager = opRoutineManager;
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        public void Edit_Back()
        {
            var nodeDto = _nodeRepository.GetNodeDto(MixedFeedNodeId);
            if (nodeDto == null) return;

            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.Workstation;
            UpdateNodeContext(nodeDto.Context);
        }

        public MixedFeedManageVms.EditVm GetEditVm()
        {
            var vm = new MixedFeedManageVms.EditVm();

            var nodeDto = _nodeRepository.GetNodeDto(MixedFeedNodeId);
            if (nodeDto?.Context?.OpProcessData == null) return vm;

            var silo = _context.MixedFeedSilos.FirstOrDefault(x => x.Id == nodeDto.Context.OpProcessData.Value);
            if (silo != null)
            {
                vm.Drive = _context.Nodes.FirstOrDefault(x => x.Id == silo.Drive)?.Name;
                vm.IsActive = silo.IsActive;
                vm.LoadQueue = silo.LoadQueue;
                vm.SiloEmpty = silo.SiloEmpty;
                vm.SiloFull = silo.SiloFull;
                vm.SiloWeight = silo.SiloWeight;
                vm.Id = silo.Id;
                vm.ProductId = silo.ProductId;
                vm.Specification = silo.Specification;
            }

            return vm;
        }

        public void Edit_Save(MixedFeedManageVms.EditVm mixedFeedSilo)
        {
            try
            {
                var silo = _context.MixedFeedSilos.FirstOrDefault(x => x.Id == mixedFeedSilo.Id);
                if (silo is null) return;

                silo.Specification = mixedFeedSilo.Specification;
                silo.LoadQueue = mixedFeedSilo.LoadQueue;
                silo.ProductId = mixedFeedSilo.ProductId;
                silo.SiloEmpty = mixedFeedSilo.SiloEmpty;
                silo.SiloFull = mixedFeedSilo.SiloFull;
                silo.SiloWeight = mixedFeedSilo.SiloWeight;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.Error($"MixedFeedManager: Save error: {e}");
            }
            
           
            var nodeDto = _nodeRepository.GetNodeDto(MixedFeedNodeId);
            if (nodeDto == null) return;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Context);
        }

        public MixedFeedManageVms.WorkstationSiloVm GetSiloItems()
        {
            var items = _nodeRepository.GetQuery<MixedFeedSilo, long>()
                .ToList()
                .Select(x => new SiloDetailVm
                {
                    Drive = x.Drive,
                    ProductId = x.ProductId,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    LoadQueue = x.LoadQueue,
                    SiloEmpty = x.SiloEmpty,
                    SiloFull = x.SiloFull,
                    SiloWeight = x.SiloWeight,
                    Specification = x.Specification,
                    ProductName = !string.IsNullOrEmpty(x.ProductId) ? _externalDataRepository.GetProductDetail(x.ProductId).ShortName : string.Empty
                })
                .ToList();

            return new MixedFeedManageVms.WorkstationSiloVm
            {
                Items = items
            };
        }

        public void Workstation_SelectSilo(long siloId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(MixedFeedNodeId);
            if (nodeDto == null) return;

            nodeDto.Context.OpProcessData = siloId;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.Edit;
            UpdateNodeContext(nodeDto.Context);
        }

        public void Edit_Clear()
        {
            var nodeDto = _nodeRepository.GetNodeDto(MixedFeedNodeId);
            if (nodeDto?.Context?.OpProcessData == null) return;
            try
            {
                var silo = _context.MixedFeedSilos.FirstOrDefault(x => x.Id == nodeDto.Context.OpProcessData.Value);
                if (silo is null) return;

                silo.Specification = string.Empty;
                silo.LoadQueue = 0;
                silo.ProductId = null;
                silo.SiloEmpty = 21;
                silo.SiloFull = 0;
                silo.SiloWeight = 0;
                _context.SaveChanges();
                
                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa;
                UpdateNodeContext(nodeDto.Context);
            }
            catch (Exception e)
            {
                _logger.Error($"MixedFeedManager: Clear error: {e}");
            }
        }
        
        private void UpdateNodeContext(NodeContext newContext)
        {
            var result = _nodeRepository.UpdateNodeContext(MixedFeedNodeId, newContext, Dom.OpRoutine.Processor.WebUI);

            if (!result) 
            {
                _opRoutineManager.UpdateProcessingMessage(MixedFeedNodeId, 
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"Не валідна спроба зміни стану вузла."));
            }
            else
            {
                SignalRInvoke.ReloadHubGroup(MixedFeedNodeId);
            }
        }
    }
}