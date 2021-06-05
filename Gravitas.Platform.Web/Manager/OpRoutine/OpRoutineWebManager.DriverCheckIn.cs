using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Platform.Web.ViewModel.OpRoutine.DriverCheckIn;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public void DriverCheckIn_Idle_CheckIn(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.AddDriver;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void DriverCheckIn_CheckIn_Idle(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void DriverCheckIn_CheckIn_DriverInfoCheck(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.DriverInfoCheck;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void DriverCheckIn_DriverInfoCheck_RegistrationConfirm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.RegistrationConfirm;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void DriverCheckIn_AddDriver(DriverCheckInVms.AddDriverVm model)
        {
            var nodeDto = _nodeRepository.GetNodeDto(model.NodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;
            
            if (model.PhoneNumber.Length != 7)
            {
                _opRoutineManager.UpdateProcessingMessage(model.NodeId, new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Невірний номер телефону"));
                return;
            }
            var dataModel = new DriverCheckInOpData
            {
                OrderNumber = _context.DriverCheckInOpDatas.Count() + 1,
                CheckInDateTime = DateTime.Now,
                DriverPhotoId = _context.DriverPhotos.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber)?.Id,
                PhoneNumber = model.PhoneNumber
            };

            var singleWindowPhone = $"+380{model.PhoneNumber}";
            var lastProcessed = _context.SingleWindowOpDatas
                .Where(x => x.ContactPhoneNo == singleWindowPhone)
                .OrderByDescending(x => x.CreateDate)
                .FirstOrDefault();

            if (lastProcessed != null)
            {
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
            }

            _context.DriverCheckInOpDatas.Add(dataModel);
            _context.SaveChanges();

            nodeDto.Context.OpProcessData = dataModel.Id;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.DriverInfoCheck;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public DriverCheckInVms.DriverInfoCheckVm GetDriverInfoCheckModel(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return null;

            return _context.DriverCheckInOpDatas
                .Where(x => x.Id == nodeDto.Context.OpProcessData.Value)
                .Select(x => new DriverCheckInVms.DriverInfoCheckVm
                {
                    Driver = x.Driver,
                    NodeId = nodeId,
                    PhoneNumber = x.PhoneNumber,
                    PhotoUrl = x.DriverPhoto.ImagePath,
                    Trailer = x.Trailer,
                    Truck = x.Truck
                })
                .FirstOrDefault();
        }

        public void DriverCheckIn_DriverInfoCheck(DriverCheckInVms.DriverInfoCheckVm model)
        {
            var nodeDto = _nodeRepository.GetNodeDto(model.NodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;
            
            var opData = _context.DriverCheckInOpDatas.First(x => x.PhoneNumber == model.PhoneNumber);

            opData.Driver = model.Driver;
            opData.Truck = model.Truck;
            opData.Trailer = model.Trailer;

            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.DriverCheckIn.State.RegistrationConfirm;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public DriverCheckInVms.RegistrationConfirmVm GetRegistrationConfirm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return null;

            return _context.DriverCheckInOpDatas
                .Where(x => x.Id == nodeDto.Context.OpProcessData.Value)
                .Select(x => new DriverCheckInVms.RegistrationConfirmVm
                {
                    NodeId = nodeId,
                    OrderNumber = x.OrderNumber
                })
                .FirstOrDefault();
        }
    }
}