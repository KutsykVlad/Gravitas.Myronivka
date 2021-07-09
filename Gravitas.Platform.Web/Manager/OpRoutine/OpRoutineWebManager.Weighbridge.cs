using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{

    public partial class OpRoutineWebManager
    {
        private WeightbridgeVms.WeightingPromptVm GetWeightingPromptVm(Model.DomainModel.Node.TDO.Detail.NodeDetails nodeDetailsDto, ScaleOpData scaleOpData, ScaleOpData previousScaleData)
        {
            var result = new WeightbridgeVms.WeightingPromptVm(GetBaseWeightPromptVm(nodeDetailsDto, scaleOpData));
            if (scaleOpData.TypeId == ScaleOpDataType.Tare)
            {
                if (previousScaleData != null)
                {
                    result.GrossValue = previousScaleData.TruckWeightValue;
                    result.TrailerGrossValue = previousScaleData.TrailerWeightValue;
                }

                if (scaleOpData.TruckWeightValue != null) result.TareValue = scaleOpData.TruckWeightValue;
            }
            else
            {
                if (previousScaleData != null)
                {
                    result.TareValue = previousScaleData.TruckWeightValue;
                    result.TrailerTareValue = previousScaleData.TrailerWeightValue;
                }

                if (scaleOpData.TruckWeightValue != null) result.GrossValue = scaleOpData.TruckWeightValue;
            }

            result.IsSecondWeighting = previousScaleData != null;
            return result;
        }

        private WeightbridgeVms.BaseWeightPromptVm GetBaseWeightPromptVm(Model.DomainModel.Node.TDO.Detail.NodeDetails nodeDetailsDto, ScaleOpData scaleOpData)
        {
            var vm = new WeightbridgeVms.BaseWeightPromptVm();
            var windowInOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDetailsDto.Context.TicketId);
            if (windowInOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Помилка. Дані операції не знайдено"));
                return vm;
            }

            var windowOpDataDetail = _opDataManager.GetSingleWindowOpDataDetail(windowInOpData.Id);
            if (windowOpDataDetail == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Помилка. Дані операції не знайдено"));
                return vm;
            }

            vm.NodeId = nodeDetailsDto.Id;
            vm.ScaleOpTypeName = scaleOpData.TypeId == ScaleOpDataType.Tare ? "Тара" : "Брутто";

            vm.ProductName = _context.Products.FirstOrDefault(x => x.Id == windowInOpData.ProductId)?.ShortName ??
                             _context.Products.FirstOrDefault(x => x.Id == windowInOpData.ProductId)?.FullName ??
                             windowOpDataDetail.ProductName;
            vm.ReceiverName = _context.Stocks.FirstOrDefault(x => x.Id == windowInOpData.ReceiverId)?.ShortName
                              ?? _context.Subdivisions.FirstOrDefault(x => x.Id == windowInOpData.ReceiverId)?.ShortName
                              ?? _context.Partners.FirstOrDefault(x => x.Id == windowInOpData.ReceiverId)?.ShortName
                              ?? windowInOpData.CustomPartnerName;

            vm.IncomeDocTareValue = windowInOpData.IncomeDocTareValue;
            vm.IncomeDocGrossValue = windowInOpData.IncomeDocGrossValue;
            vm.DocNetValue = windowInOpData.DocNetValue;

            if (windowInOpData.IsThirdPartyCarrier)
            {
                vm.TruckNo = windowInOpData.TransportNumber;
                vm.TrailerNo = windowInOpData.TrailerNumber;
                vm.DriverName = windowInOpData.HiredDriverCode;
            }
            else
            {
                vm.TruckNo = _context.FixedAssets.FirstOrDefault(x => x.Id == windowInOpData.TransportId)?.RegistrationNo;
                vm.TrailerNo = _context.FixedAssets.FirstOrDefault(x => x.Id == windowInOpData.TrailerId)?.RegistrationNo;
                vm.DriverName =
                    $"{_context.Employees.FirstOrDefault(x => x.Id == windowInOpData.DriverOneId)?.ShortName} / " +
                    $"{_context.Employees.FirstOrDefault(x => x.Id == windowInOpData.DriverTwoId)?.ShortName}";
            }

            return vm;
        }

        public WeightbridgeVms.BaseWeightPromptVm Weighbridge_GetWeightPrompt_GetData(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            var result = new WeightbridgeVms.BaseWeightPromptVm();

            if (!IsValidNodeContext(nodeDto)) return result;

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) return result;

            result = GetBaseWeightPromptVm(nodeDto, scaleOpData);

            return result;
        }

        public void Weighbridge_DriverTrailerAccepted(int nodeId, bool isTrailerAccepted)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return;

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) return;

            if (scaleOpData.TrailerIsAvailable)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Info, @"Автомобіль має зважуватися з розчепленням"));
                return;
            }

            if (isTrailerAccepted)
            {
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GuardianCardPrompt;
                scaleOpData.GuardianPresence = true;
                scaleOpData.TrailerIsAvailable = true;
                nodeDto.Context.OpProcessData = null;

                var card = _context.Cards.First(x => x.TicketContainerId == nodeDto.Context.TicketContainerId);
                _connectManager.SendSms(SmsTemplate.InvalidPerimeterGuardianSms, nodeDto.Context.TicketId, _phonesRepository.GetPhone(Phone.Security), cardId: card.Id);
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Info, @"Очікуйте прибуття охоронця для подальшого зважування"));
            }
            else
            {
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.TruckWeightPrompt;
                scaleOpData.TrailerWeightValue = 0;
                scaleOpData.TrailerWeightIsAccepted = true;
            }

            _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void Weighbridge_GuardianTruckVerification_Process(int nodeId, bool isWeighingAllowed)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return;

            if (!isWeighingAllowed)
            {
                var previousScaleData = _context.ScaleOpDatas.Where(t =>
                    t.TicketId == nodeDto.Context.TicketId && (t.StateId == OpDataState.Processed || t.StateId == OpDataState.Rejected))
                    .OrderByDescending(x => x.CheckInDateTime)
                    .FirstOrDefault();
                if (previousScaleData != null)
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                        ProcessingMsgType.Error, @"Помилка. Охоронець не може заборонити зважування на даному етапі"));
                    return;
                }

                var nonStandardOpData = new NonStandartOpData
                {
                    NodeId = nodeDto.Id,
                    TicketContainerId = nodeDto.Context.TicketContainerId,
                    TicketId = nodeDto.Context.TicketId,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    StateId = OpDataState.Processed,
                    Message = "Охоронець не дозволив зважування"
                };
                _opDataRepository.Add<NonStandartOpData, Guid>(nonStandardOpData);

                if (nodeDto.Context.OpDataId != null)
                {
                    var scaleOpData = _context.ScaleOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
                    scaleOpData.StateId = OpDataState.Rejected;
                    _context.SaveChanges();
                }

                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GetTruckWeight;
            }
            else
            {
                var scale = _opDataRepository.GetFirstOrDefault<ScaleOpData, Guid>(t =>
                        t.TicketId == nodeDto.Context.TicketId.Value && (t.StateId == OpDataState.Processed || t.StateId == OpDataState.Rejected));

                nodeDto.Context.OpRoutineStateId = scale != null
                    ? Model.DomainValue.OpRoutine.Weighbridge.State.TruckWeightPrompt
                    : Model.DomainValue.OpRoutine.Weighbridge.State.GuardianTrailerEnableCheck;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void Weighbridge_GuardianTrailerEnable_Process(int nodeId, bool isTrailerEnabled)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return;

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Error, @"Помилка. Дані операції не знайдено"));

                return;
            }

            if (isTrailerEnabled)
            {
                scaleOpData.TrailerIsAvailable = true;
                nodeDto.Context.OpProcessData = null;
            }
            else
            {
                scaleOpData.TrailerWeightValue = 0;
                scaleOpData.TrailerWeightIsAccepted = true;
            }

            _nodeRepository.Update<ScaleOpData, Guid>(scaleOpData);
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.TruckWeightPrompt;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public WeightbridgeVms.TruckWeightPromptVm WeighbridgeGetTruckWeightPromptVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return new WeightbridgeVms.TruckWeightPromptVm();

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null)
            {
                return new WeightbridgeVms.TruckWeightPromptVm();
            }

            var previousScaleData = _context.ScaleOpDatas.Where(
                x => x.TicketId == nodeDto.Context.TicketId 
                     && x.TypeId != scaleOpData.TypeId
                     && (x.StateId == OpDataState.Processed || x.StateId == OpDataState.Rejected))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();

            var result = new WeightbridgeVms.TruckWeightPromptVm(GetWeightingPromptVm(nodeDto, scaleOpData, previousScaleData));

            if (scaleOpData.TrailerIsAvailable == false && (previousScaleData == null || previousScaleData.TypeId == ScaleOpDataType.Tare))
            {
                result.IsSelectionAvailable = true;
            }
            else
            {
                result.IsSelectionAvailable = false;
            }

            return result;
        }

        public WeightbridgeVms.GetGuardianTruckWeightPermissionVm Weighbridge__GetGuardianTruckWeightPermissionVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return new WeightbridgeVms.GetGuardianTruckWeightPermissionVm();

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) return new WeightbridgeVms.GetGuardianTruckWeightPermissionVm();

            var previousScaleData = _opDataRepository.GetFirstOrDefault<ScaleOpData, Guid>(t =>
                t.TicketId == nodeDto.Context.TicketId && t.StateId == OpDataState.Processed);

            var result = new WeightbridgeVms.GetGuardianTruckWeightPermissionVm(GetWeightingPromptVm(nodeDto, scaleOpData, previousScaleData));

            return result;
        }

        public WeightbridgeVms.TrailerWeightPromptVm Weighbridge__GetTrailerWeightPromptVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return new WeightbridgeVms.TrailerWeightPromptVm();

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) return new WeightbridgeVms.TrailerWeightPromptVm();

            var previousScaleData = _opDataRepository.GetFirstOrDefault<ScaleOpData, Guid>(t =>
                t.TicketId == nodeDto.Context.TicketId && t.StateId == OpDataState.Processed);

            var result = new WeightbridgeVms.TrailerWeightPromptVm(GetWeightingPromptVm(nodeDto, scaleOpData, previousScaleData));

            if (previousScaleData != null)
            {
                result.IsRejectButtonDisabled = true;
            }
            
            if (previousScaleData == null || previousScaleData.TypeId == ScaleOpDataType.Tare)
            {
                result.IsSelectionAvailable = true;
            }
            else
            {
                result.IsSelectionAvailable = false;
            }
           
            return result;
        }

        public WeightbridgeVms.GetGuardianTrailerWeightPermissionVm Weighbridge__GetGuardianTrailerWeightPermissionVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return new WeightbridgeVms.GetGuardianTrailerWeightPermissionVm();

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) return new WeightbridgeVms.GetGuardianTrailerWeightPermissionVm();

            var previousScaleData = _opDataRepository.GetFirstOrDefault<ScaleOpData, Guid>(t =>
                t.TicketId == nodeDto.Context.TicketId && t.StateId == OpDataState.Processed);
            var result = new WeightbridgeVms.GetGuardianTrailerWeightPermissionVm(GetWeightingPromptVm(nodeDto, scaleOpData, previousScaleData));
            return result;
        }

        public WeightbridgeVms.OpenBarrierOutVm Weighbridge_OpenBarrierOutVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            var vm = new WeightbridgeVms.OpenBarrierOutVm
            {
                NodeId = nodeId
            };

            if (nodeDto?.Context?.OpDataId == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Помилка. Хибний номер вузла"));
                return vm;
            }

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) return vm;

            vm.IsSmsNecessary = !_routesInfrastructure.IsTicketRejected(scaleOpData.TicketId.Value);
            
            return vm;
        }

        public void Weighbridge_ResetWeighbridge(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (nodeDto == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Помилка. Хибний номер вузла"));
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.Idle;
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void Weighbridge_GetWeightPrompt_Process(int nodeId, bool isWeightAccepted, bool isTruckWeighting)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (!IsValidNodeContext(nodeDto)) return;

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Помилка. Дані операції не знайдено"));
                return;
            }

            if (isWeightAccepted)
            {
                if (isTruckWeighting)
                {
                    nodeDto.Context.OpRoutineStateId = scaleOpData.GuardianPresence
                        ? Model.DomainValue.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission
                        : Model.DomainValue.OpRoutine.Weighbridge.State.GetTruckWeight;
                }
                else
                {
                    nodeDto.Context.OpRoutineStateId = scaleOpData.GuardianPresence
                        ? Model.DomainValue.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission
                        : Model.DomainValue.OpRoutine.Weighbridge.State.GetTrailerWeight;
                }
                
                var isTareMoreGross = _scaleManager.IsTareMoreGross(nodeDto, isTruckWeighting, scaleOpData);

                if (isTareMoreGross)
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                        new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Тара більша за брутто. Проведіть повторне зважування"));
                    scaleOpData.StateId = OpDataState.Canceled;
                    nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.OpenBarrierOut;
                }
            }
            else
            {
                var nonStandardOpData = new NonStandartOpData
                {
                    NodeId = nodeDto.Id,
                    TicketContainerId = nodeDto.Context.TicketContainerId,
                    TicketId = nodeDto.Context.TicketId,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    StateId = OpDataState.Processed,
                    Message = "Відмова від зважування."
                };
                _opDataRepository.Add<NonStandartOpData, Guid>(nonStandardOpData);
            }
            
            if (isTruckWeighting)
            {
                scaleOpData.TruckWeightIsAccepted = isWeightAccepted;
            }
            else
            {
                scaleOpData.TrailerWeightIsAccepted = isWeightAccepted;
            }

            scaleOpData.CheckOutDateTime = DateTime.Now;
            _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);

            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private bool IsValidNodeContext(Model.DomainModel.Node.TDO.Detail.NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto == null) return false;
            if (nodeDetailsDto.Context?.TicketContainerId != null
                && nodeDetailsDto.Context?.TicketId != null
                && nodeDetailsDto.Context?.OpDataId != null) return true;

            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Помилка. Хибний контекст"));
            return false;
        }
    }
}