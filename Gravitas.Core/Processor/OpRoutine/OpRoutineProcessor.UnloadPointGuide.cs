using System;
using System.Data.Entity.Migrations;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Core.Processor.OpRoutine {

	class UnloadPointGuideOpRoutineProcessor : BaseOpRoutineProcessor {

		private readonly IConnectManager _connectManager;
		private readonly IUserManager _userManager;
		private readonly IUnloadPointManager _unloadPointManager;
		private readonly IQueueInfrastructure _queueInfrastructure;
		private readonly ITicketRepository _ticketRepository;

        public UnloadPointGuideOpRoutineProcessor(
			IOpRoutineManager opRoutineManager, 
			IDeviceManager deviceManager,
			IDeviceRepository deviceRepository,
			INodeRepository nodeRepository,
			IOpDataRepository opDataRepository,
			IConnectManager connectManager, 
			IUserManager userManager, 
			IUnloadPointManager unloadPointManager,
			IQueueInfrastructure queueInfrastructure, 
			ITicketRepository ticketRepository) : 
			base(opRoutineManager,
				deviceManager,
				deviceRepository, 
				nodeRepository,
				opDataRepository)
        {
			_connectManager = connectManager;
	        _userManager = userManager;
	        _unloadPointManager = unloadPointManager;
	        _queueInfrastructure = queueInfrastructure;
	        _ticketRepository = ticketRepository;
        }

		public override bool ValidateNodeConfig(NodeConfig config) {

			if (config == null) {
				return false;
			}

			var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader);

			return rfidValid;
		}
		
		public override void Process() {

			ReadDbData();
			if (!ValidateNode(_nodeDto)) {
				return;
			}

			switch (_nodeDto.Context.OpRoutineStateId) {
				case Dom.OpRoutine.UnloadPointGuide.State.Idle:
					break;
				case Dom.OpRoutine.UnloadPointGuide.State.BindUnloadPoint:
					break;
				case Dom.OpRoutine.UnloadPointGuide.State.AddOpVisa:
					AddOperationVisa(_nodeDto);
					break;
				case Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa:
					EntryAddOpVisa(_nodeDto);
					break;
			}
		}
		
		private void AddOperationVisa(Node nodeDto) {

			if (nodeDto?.Context?.TicketId == null) return;
			
			var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
			if (card == null) return;

			var unloadResult = _unloadPointManager.ConfirmUnloadGuide(nodeDto.Context.TicketId.Value, card.EmployeeId);
			if (!unloadResult) return;

			if (!_connectManager.SendSms(Dom.Sms.Template.DestinationPointApprovalSms, nodeDto.Context.TicketId))
		    {
                Logger.Error("Sms hasn`t been sent");
		    }
			
			nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle;
			UpdateNodeContext(nodeDto.Id, nodeDto.Context);
		}	
		
		private void EntryAddOpVisa(Node nodeDto) {

			if (nodeDto?.Context?.TicketContainerId == null) return;
			
			var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
			if (card == null) return;
			
			_queueInfrastructure.ImmediateEntrance(nodeDto.Context.TicketContainerId.Value);
			var ticketId = _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, Dom.Ticket.Status.ToBeProcessed)?.Id;
			_connectManager.SendSms(Dom.Sms.Template.EntranceApprovalSms, ticketId);

			var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId);

			var unloadVisa = new OpVisa
			{
				DateTime = DateTime.Now,
				Message = "Дозволений в'їзд на територію",
				SingleWindowOpDataId = singleWindowOpData.Id,
				EmployeeId = card.EmployeeId,
				OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa
			};
			_ticketRepository.Add<OpVisa, long>(unloadVisa);

			nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle;
			UpdateNodeContext(nodeDto.Id, nodeDto.Context);
		}
	}
}
