using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.OpData.DAO;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class DriveCheckInOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardManager _cardManager;

        public DriveCheckInOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ICardManager cardManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _cardManager = cardManager;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.DriverCheckIn.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.DriverCheckIn.State.AddDriver:
                    AddDriver(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.DriverCheckIn.State.DriverInfoCheck:
                    break;
                case Model.DomainValue.OpRoutine.DriverCheckIn.State.RegistrationConfirm:
                    RegistrationConfirm(NodeDetails);
                    break;
            }
        }

        private void AddDriver(NodeDetails nodeDetailsDto)
        {
            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) return;
            
            var dataModel = new DriverCheckInOpData
            {
                OrderNumber = _context.DriverCheckInOpDatas.Count() + 1,
                CheckInDateTime = DateTime.Now
            };

            var lastProcessed = _context.SingleWindowOpDatas
                .Where(x => x.TicketId == card.Ticket.Id)
                .OrderByDescending(x => x.CreateDate)
                .First();

            dataModel.PhoneNumber = lastProcessed.ContactPhoneNo;
            dataModel.DriverPhotoId = lastProcessed.DriverPhotoId;

            if (lastProcessed.IsThirdPartyCarrier)
            {
                dataModel.Truck = lastProcessed.HiredTransportNumber;
                dataModel.Trailer = lastProcessed.HiredTrailerNumber;
                dataModel.Driver = lastProcessed.HiredDriverCode;
            }
            else
            {
                dataModel.Truck = _context.FixedAssets.FirstOrDefault(x => x.Id == lastProcessed.TransportId)?.RegistrationNo;
                dataModel.Trailer = _context.FixedAssets.FirstOrDefault(x => x.Id == lastProcessed.TrailerId)?.RegistrationNo;
                dataModel.Driver =
                    $"{_context.Employees.FirstOrDefault(x => x.Id == lastProcessed.DriverOneId)?.ShortName} / " +
                    $"{_context.Employees.FirstOrDefault(x => x.Id == lastProcessed.DriverTwoId)?.ShortName}";
            }

            _context.DriverCheckInOpDatas.Add(dataModel);
            _context.SaveChanges();
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.DriverInfoCheck;
            nodeDetailsDto.Context.OpProcessData = dataModel.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        private void RegistrationConfirm(NodeDetails nodeDetails)
        {
            Thread.Sleep(TimeSpan.FromSeconds(15));
            
            nodeDetails.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.Idle;
            nodeDetails.Context.OpProcessData = null;
            UpdateNodeContext(nodeDetails.Id, nodeDetails.Context);
        }
    }
}