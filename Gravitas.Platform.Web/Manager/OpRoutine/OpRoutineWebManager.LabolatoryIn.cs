using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExternalData = Gravitas.Model.ExternalData;
using LabFacelessOpData = Gravitas.Model.LabFacelessOpData;
using LabFacelessOpDataComponent = Gravitas.Model.LabFacelessOpDataComponent;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public LaboratoryInVms.PrintCollisionManageVm GetLaboratoryIn_PrintCollisionManageVm(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return null;

            var opDataState = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value)?.StateId;

            var vm = new LaboratoryInVms.PrintCollisionManageVm
            {
                NodeId = nodeId,
                OpDataId = nodeDto.Context.OpDataId.Value,
                SamplePrintoutVm = LaboratoryIn_SamplePrintout_GetVm(nodeDto.Context.OpDataId.Value),
                ReasonsForRefund = _externalDataRepository.GetQuery<ExternalData.ReasonForRefund, string>().ToList(),
                OpDataState = opDataState.HasValue ? _context.OpDataStates.First(x => x.Id == opDataState.Value).Name : string.Empty,
                IsLabFile = _ticketRepository.GetTicketFilesByType(Dom.TicketFile.Type.LabCertificate).Any(item => item.TicketId == nodeDto.Context.TicketId)
            };

            return vm;
        }

        public bool LaboratoryIn_PrintCollisionInit_Return(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public LaboratoryInVms.PrintLaboratoryProtocol PrintLaboratoryProtocol_GetVm(long nodeId)
        {
            var result = new LaboratoryInVms.PrintLaboratoryProtocol
            {
                NodeId = nodeId
            };
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return result;
            result.OpDataId = nodeDto.Context.OpDataId.Value;
            return result;
        }

        public void PrintLaboratoryProtocol_Next(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return;
            
            nodeDto.Context.TicketId = null;
			nodeDto.Context.TicketContainerId = null;
			nodeDto.Context.OpDataId = null;
			nodeDto.Context.OpDataComponentId = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_Idle_SelectTicketContainer(long nodeId, long ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            var tmpTicket = _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.Processing)
                            ?? _ticketRepository.GetTicketInContainer(ticketContainerId,
                                Dom.Ticket.Status.ToBeProcessed)
                            ?? _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.New);

            var opData = _opDataRepository.GetLastOpData(tmpTicket.Id, Dom.OpDataState.CollisionApproved)
                         ?? _opDataRepository.GetLastOpData(tmpTicket.Id, Dom.OpDataState.CollisionDisapproved);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = tmpTicket.Id;
            nodeDto.Context.OpDataId = opData.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_Idle_СollectSample(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_Idle_EditAnalysisResult(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.ResultReadTrayRfid;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_Idle_PrintAnalysisResult(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintReadTrayRfid;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_SampleReadTruckRfid_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_SampleBindTray_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public bool LaboratoryIn_Idle_PrintCollisionInit(long nodeId, int ticketId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            var opDataId = _context.LabFacelessOpDatas
                .Where(x => x.TicketId == ticketId)
                .Select(x => x.Id)
                .First();
            
            nodeDto.Context.OpDataId = opDataId;
            nodeDto.Context.TicketId = ticketId;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public LaboratoryInVms.SampleBindAnalysisTrayVm LaboratoryIn_SampleBindAnalysisTray_GetVmData(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null || nodeDto.Context?.TicketId == null) return null;
            var opData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.TicketId == nodeDto.Context.TicketId.Value);
            var card = _context.Cards.FirstOrDefault(e =>
                    e.TicketContainerId == nodeDto.Context.TicketContainerId && e.TypeId == Dom.Card.Type.TicketCard);

            var analysisValueDescriptor = new LaboratoryInVms.AnalysisValueDescriptorVm
            {
                EditHumidity = true,
                EditImpurity = true,
                EditEffectiveValue = true,
                EditIsInfectioned = true
            };
            var routineData = new LaboratoryInVms.SampleBindAnalysisTrayVm
            {
                NodeId = nodeId,
                OpDataId = nodeDto.Context.OpDataId.Value,
                AnalysisValueDescriptor = analysisValueDescriptor,
                Product = _externalDataRepository.GetProductDetail(opData.ProductId)?.ShortName ?? string.Empty,
                Card = card != null ? card.No.ToString() : string.Empty
            };

            return routineData;
        }

        public bool LaboratoryIn_SampleBindAnalysisTray(LaboratoryInVms.SampleBindAnalysisTrayVm vmData)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vmData.NodeId);
            // Validate context
            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context?.TicketId == null
                || nodeDto.Context?.OpDataId == null)
                return false;
            
            if (!vmData.AnalysisValueDescriptor.EditHumidity
                && !vmData.AnalysisValueDescriptor.EditImpurity
                && !vmData.AnalysisValueDescriptor.EditEffectiveValue
                && !vmData.AnalysisValueDescriptor.EditIsInfectioned)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error,@"Виберіть значення"));
                return false;
            }

            var sampleCard = _context.Cards.FirstOrDefault(c =>
                c.TicketContainerId == nodeDto.Context.TicketContainerId.Value 
                && c.TypeId == Dom.Card.Type.LaboratoryTray
                && c.Id == c.ParentCardId);

            if (sampleCard == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error, @"Немає прив'язаного лотка"));
                return false;
            }

            var analysisCards = _context.Cards.Where(c => c.ParentCardId == sampleCard.Id && c.Id != sampleCard.Id).ToList();

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return false;

            foreach (var analysisCard in analysisCards)
            {
                analysisCard.TicketContainerId = nodeDto.Context.TicketContainerId.Value;
                _cardRepository.Update<Card, string>(analysisCard);

                var component = _context.LabFacelessOpDataComponents.FirstOrDefault(x =>
                                    x.LabFacelessOpDataId == nodeDto.Context.OpDataId
                                    && x.AnalysisTrayRfid == analysisCard.Id) ?? new LabFacelessOpDataComponent
                                {
                                    LabFacelessOpDataId = labFacelessOpData.Id,
                                    StateId = Dom.OpDataState.Init,
                                    AnalysisTrayRfid = analysisCard.Id
                                };
                component.AnalysisValueDescriptor = Mapper.Map<AnalysisValueDescriptor>(vmData.AnalysisValueDescriptor)?.ToJson();
                _opDataRepository.AddOrUpdate<LabFacelessOpDataComponent, long>(component);
            }

            _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Success, @"Лотки прив'язано"));
            SignalRInvoke.ReloadHubGroup(nodeDto.Id);
            
            return true;
        }

        public bool LaboratoryIn_SampleBindAnalysisTray_Next(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            var areOpDataComponents = _context.LabFacelessOpDataComponents.Any(item =>
                    item.LabFacelessOpDataId == nodeDto.Context.OpDataId);

            if (areOpDataComponents)
            {
                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.SampleAddOpVisa;
                _nodeRepository.ClearNodeProcessingMessage(nodeId);
                return UpdateNodeContext(nodeDto.Id, nodeDto.Context); 
            }
            _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                Dom.Node.ProcessingMsg.Type.Warning, @"Немає прив'язаних лотків"));
            return false;
        }

        public bool LaboratoryIn_ResultReadTrayRfid_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public LaboratoryInVms.ResultEditAnalysisVm LaboratoryIn_ResultEditAnalysis_GetVm(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null
                || nodeDto.Context?.OpDataId == null
                || nodeDto.Context?.OpDataComponentId == null)
                return null;

            var labFacelessOpDataDto =
                _opDataManager.GetLabFacelessOpDataDto(nodeDto.Context.OpDataId.Value);

            var singleWindowOpDataDto =
                _opDataManager.GetSingleWindowOpDataDetail(
                    _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId)?.Id ?? Guid.Empty);

            var labFacelessOpDataComponentDto =
                labFacelessOpDataDto.LabFacelessOpDataItemSet.FirstOrDefault(e =>
                    e.Id == nodeDto.Context.OpDataComponentId.Value);

            if (labFacelessOpDataComponentDto == null) return null;

            var routineData = new LaboratoryInVms.ResultEditAnalysisVm
            {
                NodeId = nodeId,
                OpDataId = nodeDto.Context.OpDataId.Value,
                OpDataComponentId = nodeDto.Context.OpDataComponentId.Value,

                ProductName = singleWindowOpDataDto.ProductName,
                DocHumidityValue = (float?) singleWindowOpDataDto.DocHumidityValue,
                DocImpurityValue = (float?) singleWindowOpDataDto.DocImpurityValue,

                IsLabDevicesEnable = labFacelessOpDataComponentDto.AnalysisValueDescriptor.EditEffectiveValue,

                LabFacelessOpDataDetail = new LaboratoryInVms.LabFacelessOpDataDetailVm
                {
                    Id = labFacelessOpDataDto.Id,
                    NodeId = labFacelessOpDataDto.NodeId,
                    TicketId = labFacelessOpDataDto.TicketId,
                    StateId = labFacelessOpDataDto.StateId,
                    CheckInDateTime = labFacelessOpDataDto.CheckInDateTime,
                    CheckOutDateTime = labFacelessOpDataDto.CheckOutDateTime,

                    ImpurityValue = labFacelessOpDataDto.ImpurityValue,
                    HumidityValue = labFacelessOpDataDto.HumidityValue,

                    ImpurityClassId = labFacelessOpDataDto.ImpurityClassId,
                    HumidityClassId = labFacelessOpDataDto.HumidityClassId,
                    IsInfectionedClassId = labFacelessOpDataDto.InfectionedClassId,
                    EffectiveValue = labFacelessOpDataDto.EffectiveValue,
                    Comment = labFacelessOpDataDto.Comment,

                    LabFacelessOpDataComponentItemSet = new LaboratoryInVms.LabFacelessOpDataComponentItemsVm()
                },

                LabAnalyserDevices = nodeDto.Config.LabAnalyser.Values.Select(e => e.DeviceId).ToList(),

                LabFacelessOpDataComponentDetail = new LaboratoryInVms.LabFacelessOpDataComponentDetailVm
                {
                    Id = labFacelessOpDataComponentDto.Id,
                    LabFacelessOpDataId = labFacelessOpDataComponentDto.LabFacelessOpDataId,
                    NodeId = labFacelessOpDataComponentDto.NodeId,
                    StateId = labFacelessOpDataComponentDto.StateId,
                    DataSourceName = "Ручний ввід",

                    AnalysisValueDescriptor = new LaboratoryInVms.AnalysisValueDescriptorVm
                    {
                        EditIsInfectioned = labFacelessOpDataComponentDto.AnalysisValueDescriptor.EditIsInfectioned,
                        EditHumidity = labFacelessOpDataComponentDto.AnalysisValueDescriptor.EditHumidity,
                        EditEffectiveValue = labFacelessOpDataComponentDto.AnalysisValueDescriptor.EditEffectiveValue,
                        EditImpurity = labFacelessOpDataComponentDto.AnalysisValueDescriptor.EditImpurity
                    }
                }
            };

            return routineData;
        }

        public bool LaboratoryIn_ResultEditAnalysis_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public bool LaboratoryIn_ResultEditAnalysis_Save(LaboratoryInVms.LabFacelessOpDataComponentDetailVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context == null) return false;

            var dto = new Model.Dto.LabFacelessOpDataComponent
            {
                Id = vm.Id,

                LabFacelessOpDataId = vm.LabFacelessOpDataId,
                StateId = vm.StateId,
                NodeId = vm.NodeId,

                AnalysisTrayRfid = vm.AnalysisTrayRfid,
                AnalysisValueDescriptor = Mapper.Map<AnalysisValueDescriptor>(vm.AnalysisValueDescriptor),

                ImpurityClassId = vm.ImpurityClassId,
                ImpurityValue = vm.ImpurityValue,
                HumidityClassId = vm.HumidityClassId,
                HumidityValue = vm.HumidityValue,
                InfectionedClassId = vm.InfectionedClassId,
                EffectiveValue = vm.EffectiveValue,
                Comment = vm.Comment,
                DataSourceName = "Ручний ввід"
            };

            var dao = _context.LabFacelessOpDataComponents.FirstOrDefault(x => x.Id == dto.Id);

            if (dao == null) return false;

            dao.ImpurityClassId = dto.ImpurityClassId;
            dao.ImpurityValue = dto.ImpurityValue;
            dao.HumidityClassId = dto.HumidityClassId;
            dao.HumidityValue = dto.HumidityValue;
            dao.InfectionedClassId = dto.InfectionedClassId;
            dao.EffectiveValue = dto.EffectiveValue;
            dao.Comment = dto.Comment;
            dao.DataSourceName = dto.DataSourceName;
            dao.CheckInDateTime = DateTime.Now;

            _context.SaveChanges();

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_ResultEditAnalysis_SaveFromDevice(DeviceStateVms.LabAnalyserStateDialogVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context?.OpDataId == null
                || nodeDto.Context?.OpDataComponentId == null)
                return false;

            var dao = _context.LabFacelessOpDataComponents.First(x => x.Id == nodeDto.Context.OpDataComponentId.Value);

            if (dao == null) return false;

            dao.ImpurityClassId = null;
            dao.ImpurityValue = null;
            dao.HumidityClassId = null;
            dao.HumidityValue = null;
            dao.InfectionedClassId = null;
            dao.EffectiveValue = null;
            dao.Comment = string.Empty;

            foreach (var valueVm in vm.ValueList)
                switch (valueVm.TargetId)
                {
                    case Dom.ExternalData.LabDevResultType.Ignore:
                        break;
                    case Dom.ExternalData.LabDevResultType.SaveAsComment:
                        dao.Comment += $@" {valueVm.Name} = {valueVm.Value};";
                        break;
                    case Dom.ExternalData.LabDevResultType.SaveAsHumidity:
                        dao.HumidityValue = (float?) valueVm.Value;
                        break;
                    case Dom.ExternalData.LabDevResultType.SaveAsEffectiveValue:
                        dao.EffectiveValue = (float?) valueVm.Value;
                        break;
                }

            dao.DataSourceName = $@"{vm.DeviceName}";
            dao.CheckInDateTime = DateTime.Now;

            _opDataRepository.Update<LabFacelessOpDataComponent, long>(dao);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LaboratoryIn_PrintReadTrayRfid_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public LaboratoryInVms.PrintAnalysisResultsVm LaboratoryIn_PrintAnalysisResults_GetVmData(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null
                || nodeDto.Context?.OpDataId == null
                || nodeDto.Context?.OpDataComponentId == null)
                return null;

            FetchLabData(nodeDto.Context.OpDataId.Value);
            var labFacelessOpDataDto = _opDataManager.GetLabFacelessOpDataDto(nodeDto.Context.OpDataId.Value);

            var singleWindowOpDataDto =
                _opDataManager.GetSingleWindowOpDataDetail(
                    _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId.Value)?.Id ??
                    Guid.Empty);

            var dtoLabFacelessOpDataComponent =
                labFacelessOpDataDto.LabFacelessOpDataItemSet.FirstOrDefault(e =>
                    e.Id == nodeDto.Context.OpDataComponentId.Value);

            if (dtoLabFacelessOpDataComponent == null) return null;

            var routineData = new LaboratoryInVms.PrintAnalysisResultsVm
            {
                NodeId = nodeId,
                OpDataId = nodeDto.Context.OpDataId.Value,
                OpDataComponentId = nodeDto.Context.OpDataComponentId.Value,

                ProductName = singleWindowOpDataDto.ProductName,
                DocHumidityValue = (float?)singleWindowOpDataDto.DocHumidityValue,
                DocImpurityValue = (float?)singleWindowOpDataDto.DocImpurityValue,

                LabFacelessOpDataDetail = new LaboratoryInVms.LabFacelessOpDataDetailVm
                {
                    Id = labFacelessOpDataDto.Id,
                    NodeId = labFacelessOpDataDto.NodeId,
                    TicketId = labFacelessOpDataDto.TicketId,
                    StateId = labFacelessOpDataDto.StateId,
                    CheckInDateTime = labFacelessOpDataDto.CheckInDateTime,
                    CheckOutDateTime = labFacelessOpDataDto.CheckOutDateTime,

                    ImpurityValue = labFacelessOpDataDto.ImpurityValue,
                    HumidityValue = labFacelessOpDataDto.HumidityValue,
                    EffectiveValue = labFacelessOpDataDto.EffectiveValue,

                    ImpurityClassId = labFacelessOpDataDto.ImpurityClassId,
                    HumidityClassId = labFacelessOpDataDto.HumidityClassId,
                    IsInfectionedClassId = labFacelessOpDataDto.InfectionedClassId,
                    Comment = labFacelessOpDataDto.Comment,

                    DataSourceName = labFacelessOpDataDto.DataSourceName,

                    LabFacelessOpDataComponentItemSet = new LaboratoryInVms.LabFacelessOpDataComponentItemsVm
                    {
                        Items = labFacelessOpDataDto.LabFacelessOpDataItemSet.Select(e =>
                            new LaboratoryInVms.LabFacelessOpDataComponentItemVm
                            {
                                Id = e.Id,
                                DataSourceName = e.DataSourceName,
                                LabFacelessOpDataId = e.LabFacelessOpDataId,
                                StateId = e.StateId,
                                StateName = _opDataRepository.GetOpDataStateDetail(e.StateId)?.Name ?? @"Хибний стан",
                                AnalysisTrayRfid = e.AnalysisTrayRfid,
                                AnalysisValueDescriptor = Mapper.Map<LaboratoryInVms.AnalysisValueDescriptorVm>(e.AnalysisValueDescriptor),

                                ImpurityClassName = _externalDataRepository.GetLabImpurityСlassifierDetail(e.ImpurityClassId)?.Name,                                ImpurityValue = e.ImpurityValue,
                                HumidityClassName = _externalDataRepository.GetLabHumidityСlassifierDetail(e.HumidityClassId)?.Name,
                                HumidityValue = e.HumidityValue,
                                IsInfectionedClassId = _externalDataRepository.GetLabInfectionedСlassifierDetail(e.InfectionedClassId)?.Name,
                                EffectiveValue = e.EffectiveValue,
                                Comment = e.Comment,
                                CheckInDateTime = e.CheckInDateTime
                            }).OrderBy(x => x.CheckInDateTime).ToList()
                    }
                }
            };

            return routineData;
        }

        public bool LaboratoryIn_PrintAnalysisResult_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public bool LaboratoryIn_PrintAnalysisResult_Save(LaboratoryInVms.LabFacelessOpDataDetailVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context == null || !vm.NodeId.HasValue || nodeDto.Context.OpDataId == null) return false;

            var labFacelessOpData = _context.LabFacelessOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            labFacelessOpData.HumidityClassId = vm.HumidityClassId;
            labFacelessOpData.ImpurityClassId = vm.ImpurityClassId;
            labFacelessOpData.InfectionedClassId = vm.IsInfectionedClassId;
            labFacelessOpData.Comment = vm.Comment;
            labFacelessOpData.EffectiveValue = vm.EffectiveValue;
            labFacelessOpData.LabEffectiveClassifier = vm.IsEffectiveClassId;

            _context.SaveChanges();

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa;
            return UpdateNodeContext(vm.NodeId.Value, nodeDto.Context);
        }

        public LaboratoryInVms.PrintDataDiscloseVm LaboratoryIn_PrintDataDisclose_GetVm(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return null;

//            FetchLabData(nodeDto.Context.OpDataId.Value);
            var vm = new LaboratoryInVms.PrintDataDiscloseVm
            {
                NodeId = nodeId,
                OpDataId = nodeDto.Context.OpDataId.Value,
                SamplePrintoutVm = LaboratoryIn_SamplePrintout_GetVm(nodeDto.Context.OpDataId.Value),
                IsLabFile = _ticketRepository.GetTicketFilesByType(Dom.TicketFile.Type.LabCertificate).Any(item => item.TicketId == nodeDto.Context.TicketId),
                ReasonsForRefund = _externalDataRepository.GetQuery<ExternalData.ReasonForRefund, string>().ToList(),
                DocumentTypeId = _context.SingleWindowOpDatas.First(x => x.TicketId == nodeDto.Context.TicketId).DocumentTypeId
            };

            return vm;
        }

        public bool LaboratoryIn_PrintDataDisclose_Confirm(long nodeId, bool isConfirmed)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeId);
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return false;

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return false;

            if (isConfirmed)
            {
                labFacelessOpData.StateId = Dom.OpDataState.Processed;
                _opDataRepository.Update<LabFacelessOpData, Guid>(labFacelessOpData);
            }

            nodeDto.Context.OpRoutineStateId = isConfirmed
                ? Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa
                : Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit;

            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public bool LaboratoryIn_SendToCollision(LaboratoryInVms.PrintCollisionInitVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context.OpDataId == null) return false;
            _nodeRepository.ClearNodeProcessingMessage(vm.NodeId);

            if (!vm.Phone1.IsNullOrWhiteSpace() && (!vm.Phone1.All(char.IsDigit) || vm.Phone1.Length != 12)
                || !vm.Phone2.IsNullOrWhiteSpace() && (!vm.Phone2.All(char.IsDigit) || vm.Phone2.Length != 12)
                || !vm.Phone3.IsNullOrWhiteSpace() && (!vm.Phone3.All(char.IsDigit) || vm.Phone3.Length != 12))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    Dom.Node.ProcessingMsg.Type.Error, @"Не вірний формат телефонного номеру."));
                return false;
            }

            var opData = _context.LabFacelessOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            opData.StateId = Dom.OpDataState.Collision;
            if (!string.IsNullOrWhiteSpace(vm.Comment))
            {
                opData.CollisionComment = vm.Comment;
            }

            _context.SaveChanges();
            
            if (!_collisionWebManager.SendToConfirmation(vm.TicketId,
                new List<string> {vm.Phone1, vm.Phone2, vm.Phone3},
                new List<string> {vm.Email1?.Trim(), vm.Email2?.Trim(), vm.Email3?.Trim()})) return false;
            
            SignalRInvoke.StartSpinner(vm.NodeId);            
            Logger.Info("Laboratory collision send: Try send driver sms");
            if (!_connectManager.SendSms(Dom.Sms.Template.DriverQualityMatchingSms, nodeDto.Context.TicketId))
                Logger.Error("Message hasn`t been sent");
            else Logger.Info("Laboratory collision send: Driver sms send.");

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            _nodeRepository.ClearNodeProcessingMessage(vm.NodeId);
            return UpdateNodeContext(vm.NodeId, nodeDto.Context);
        }
        
        private string NormalizePhone(string phone)
        {
            phone = phone.Replace("-", string.Empty);
            return long.TryParse(phone, out _) ? $"38{phone}" : "";
        }

        public LaboratoryInVms.PrintCollisionInitVm LaboratoryIn_GetCollisionInitVm(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null) return null;

            var receiverAnalyticsId = _context.SingleWindowOpDatas.First(item =>
                                          item.TicketId == nodeDto.Context.TicketId).ReceiverAnaliticsId;
            if (receiverAnalyticsId == null) return null;

            var contract = _context.Contracts.FirstOrDefault (x => x.Id == receiverAnalyticsId);
            var manager = _externalDataRepository.GetEmployeeDetail(contract?.ManagerId);
            
            return new LaboratoryInVms.PrintCollisionInitVm
                          {
                              NodeId = nodeId,
                              TicketId = nodeDto.Context.TicketId.Value,
                              Phone2 = manager is null ? string.Empty : NormalizePhone(manager.PhoneNo),
                              Email2 = manager is null ? string.Empty : manager.Email,
                              ManagerList = _context.EmployeeRoles.Where(x => x.RoleId == (long) UserRole.LaboratoryManager)
                                  .AsEnumerable()
                                  .Select(l => _context.Employees.First(x => x.Id == l.EmployeeId))
                                  .ToDictionary(x => x.Id)
                          };
        }

        public bool LaboratoryIn_PrintCollisionManage_ReturnToCollectSamples(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpDataId == null || nodeDto.Context.TicketContainerId == null || nodeDto.Context.TicketId == null) return false;

            var opData = _context.LabFacelessOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            opData.StateId = Dom.OpDataState.Canceled;
            _context.SaveChanges();

            UnbindTrays(nodeDto.Context.TicketContainerId.Value);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            _connectManager.SendSms(Dom.Sms.Template.ReturnToCollectSamples, opData.TicketId);

            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public bool LaboratoryIn_PrintCollisionManage_SetReturnRoute(long nodeId, string indexRefundReason)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpDataId == null || nodeDto.Context.TicketContainerId == null) return false;

            if (nodeDto.Context.TicketId != null)
            {
                var opData = _context.LabFacelessOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
                opData.StateId = Dom.OpDataState.Rejected;
                _context.SaveChanges();

                var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(
                    item => item.TicketId == nodeDto.Context.TicketId.Value);
                singleWindowOpData.ReturnCauseId = indexRefundReason;
                _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);

                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa;
            }

            return UpdateNodeContext(nodeId, nodeDto.Context);
        }
        
        public void LaboratoryIn_PrintCollisionManage_SetReloadRoute(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpDataId == null || nodeDto.Context.TicketContainerId == null) return;

            if (nodeDto.Context.TicketId != null)
            {
                var opData = _context.LabFacelessOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
                opData.StateId = Dom.OpDataState.Rejected;
                _context.SaveChanges();

                nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa;
            }

            UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public LaboratoryInVms.SamplePrintoutVm LaboratoryIn_SamplePrintout_GetVm(Guid opDataId)
        {
            var vm = new LaboratoryInVms.SamplePrintoutVm();

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == opDataId);

            if (labFacelessOpData == null) return null;

            vm.CheckOutDateTime = labFacelessOpData.CheckOutDateTime;

            LabFacelessOpDataComponent tmpComponent;

            vm.ImpurityClassId = labFacelessOpData.ImpurityClassId;
            vm.ImpurityValue = labFacelessOpData.ImpurityValue;
            if (labFacelessOpData.ImpurityValueSourceId != null)
            {
                tmpComponent = _context.LabFacelessOpDataComponents.FirstOrDefault(x => x.Id == labFacelessOpData.ImpurityValueSourceId.Value);
                if (tmpComponent != null)
                    vm.ImpurityValueUserName =
                        tmpComponent.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee
                                    ?.ShortName ??
                        string.Empty;
            }

            vm.HumidityClassId = labFacelessOpData.HumidityClassId;
            vm.HumidityValue = labFacelessOpData.HumidityValue;
            if (labFacelessOpData.HumidityValueSourceId != null)
            {
                tmpComponent = _context.LabFacelessOpDataComponents.FirstOrDefault(x => x.Id == labFacelessOpData.HumidityValueSourceId.Value);
                if (tmpComponent != null)
                    vm.HumidityValueUserName =
                        tmpComponent.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee
                                    ?.ShortName ??
                        string.Empty;
            }

            vm.IsInfectionedClassId = labFacelessOpData.InfectionedClassId;
            if (labFacelessOpData.InfectionedValueSourceId != null)
            {
                tmpComponent = _context.LabFacelessOpDataComponents.First(x => x.Id == labFacelessOpData.InfectionedValueSourceId.Value);
                if (tmpComponent != null)
                    vm.IsInfectionedValueUserName =
                        tmpComponent.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee
                                    ?.ShortName ??
                        string.Empty;
            }

            vm.EffectiveValue = labFacelessOpData.EffectiveValue;
            if (labFacelessOpData.EffectiveValueSourceId != null)
            {
                tmpComponent = _context.LabFacelessOpDataComponents.FirstOrDefault(x => x.Id == labFacelessOpData.EffectiveValueSourceId.Value);
                if (tmpComponent != null)
                    vm.EffectiveValueUserName =
                        tmpComponent.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee
                                    ?.ShortName ??
                        string.Empty;
            }

            vm.SampleCollectUserName =
                labFacelessOpData.OpVisaSet.OrderBy(e => e.DateTime).FirstOrDefault()?.Employee?.ShortName ??
                string.Empty;
            vm.AnalysisReleaseUserName =
                labFacelessOpData.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee?.ShortName ??
                string.Empty;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(labFacelessOpData.TicketId);
            if (singleWindowOpData != null)
            {
                vm.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ??
                                 string.Empty;

                vm.SenderName = !string.IsNullOrWhiteSpace(singleWindowOpData.ReceiverId)
                    ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? singleWindowOpData.CustomPartnerName
                      ?? "- Хибний ключ -"
                    : string.Empty;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.TransportNo = singleWindowOpData.HiredTransportNumber;
                    vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    vm.TransportNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ??
                        string.Empty;
                    vm.TrailerNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ??
                        string.Empty;
                }
            }

            vm.EffectiveClassifier = string.IsNullOrEmpty(labFacelessOpData.LabEffectiveClassifier) 
                ? "Немає перерахунку" 
                : labFacelessOpData.LabEffectiveClassifier;
            vm.CommentAutoGenerated = labFacelessOpData.Comment;
            vm.CommentSingleWindow = singleWindowOpData?.Comments;
            vm.OpVisaItems = new PrintoutOpVisaItemsVm
            {
                Items = labFacelessOpData.OpVisaSet.Select(e => new PrintoutOpVisaItemVm
                {
                    Id = e.Id,
                    DateTime = e.DateTime,
                    Message = e.Message,
                    UserName = _externalDataRepository.GetExternalEmployeeDetail(e.EmployeeId)?.ShortName,
                    Comment = GenerateLabOpVisaComment(e.OpRoutineState, labFacelessOpData)
                }).ToList()
            };

            var componentList = _context.LabFacelessOpDataComponents.Where(e =>
                                    e.LabFacelessOpDataId == labFacelessOpData.Id).ToList();
            foreach (var component in componentList)
            {
                foreach (var opVisa in component.OpVisaSet.ToList())
                    vm.OpVisaItems.Items.Add(new PrintoutOpVisaItemVm
                    {
                        Id = opVisa.Id,
                        DateTime = opVisa.DateTime,
                        Message = opVisa.Message,
                        UserName = _externalDataRepository.GetEmployeeDetail(opVisa.EmployeeId)?.ShortName,
                        Comment = GenerateComponentComment(component)
                    });
            }

            vm.OpVisaItems.Items = vm.OpVisaItems.Items.OrderBy(e => e.DateTime).ToList();

            return vm;
        }

        private string GenerateLabOpVisaComment(OpRoutineState opRoutineState, LabFacelessOpData labFacelessOpData)
        {
            var result = string.Empty;
            switch (opRoutineState.Id)
            {
                case Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa:
                    var state = labFacelessOpData.StateId == Dom.OpDataState.Rejected ? "Відмовлено" : "Опрацьовано";
                    result += $"Коментар: {labFacelessOpData.Comment}, Стан: {state}";
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage:
                    result += labFacelessOpData.CollisionComment;
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa:
                    if (labFacelessOpData.EffectiveValue.HasValue) result += $" Масличність={labFacelessOpData.EffectiveValue.Value}";
                    if (labFacelessOpData.HumidityValue.HasValue) result += $", Вологість={labFacelessOpData.HumidityValue.Value}";
                    if (labFacelessOpData.ImpurityValue.HasValue) result += $", Смітна дом={labFacelessOpData.ImpurityValue.Value}";
                    if (labFacelessOpData.InfectionedClassId != null) result += $", Зараженість={labFacelessOpData.InfectionedClassId}";
                    break;
            }

            return result;
        }

        //TODO: Move to op data repository, or op data manager
        private void FetchLabData(Guid opDataId)
        {
            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == opDataId);

            if (labFacelessOpData == null) return;

            var componentList = labFacelessOpData.LabFacelessOpDataComponentSet
                                                 .OrderBy(e => e.CheckInDateTime).ToList();

            labFacelessOpData.ImpurityClassId = null;
            labFacelessOpData.ImpurityValue = null;
            labFacelessOpData.HumidityClassId = null;
            labFacelessOpData.HumidityValue = null;
            labFacelessOpData.InfectionedClassId = null;
            labFacelessOpData.EffectiveValue = null;
            labFacelessOpData.Comment = null;
            labFacelessOpData.DataSourceName = null;

            labFacelessOpData.ImpurityValueSourceId = null;
            labFacelessOpData.HumidityValueSourceId = null;
            labFacelessOpData.InfectionedValueSourceId = null;
            labFacelessOpData.EffectiveValueSourceId = null;

            foreach (var component in componentList)
            {
                var descriptors = (JObject)JsonConvert.DeserializeObject(component.AnalysisValueDescriptor);
                var valueDescriptor = new AnalysisValueDescriptor
                {
                    EditImpurity = descriptors["EditImpurity"].Value<bool>(),
                    EditHumidity = descriptors["EditHumidity"].Value<bool>(),
                    EditIsInfectioned = descriptors["EditIsInfectioned"].Value<bool>(),
                    EditEffectiveValue = descriptors["EditEffectiveValue"].Value<bool>()
                };

                // ClassId
                if (valueDescriptor.EditHumidity && !string.IsNullOrWhiteSpace(component.HumidityClassId))
                    labFacelessOpData.HumidityClassId = component.HumidityClassId;

                if (valueDescriptor.EditImpurity && !string.IsNullOrWhiteSpace(component.ImpurityClassId))
                    labFacelessOpData.ImpurityClassId = component.ImpurityClassId;

                if (valueDescriptor.EditIsInfectioned && !string.IsNullOrWhiteSpace(component.InfectionedClassId))
                {
                    labFacelessOpData.InfectionedClassId = component.InfectionedClassId;
                    labFacelessOpData.InfectionedValueSourceId = component.Id;
                }

                if (valueDescriptor.EditHumidity && component.HumidityValue != null)
                {
                    labFacelessOpData.HumidityValue = component.HumidityValue;
                    labFacelessOpData.HumidityClassId = component.HumidityClassId;
                    labFacelessOpData.HumidityValueSourceId = component.Id;
                }

                if (valueDescriptor.EditImpurity && component.ImpurityValue != null)
                {
                    labFacelessOpData.ImpurityValue = component.ImpurityValue;
                    labFacelessOpData.ImpurityClassId = component.ImpurityClassId;
                    labFacelessOpData.ImpurityValueSourceId = component.Id;
                }

                if (valueDescriptor.EditEffectiveValue && component.EffectiveValue != null)
                {
                    labFacelessOpData.EffectiveValue = component.EffectiveValue;
                    labFacelessOpData.EffectiveValueSourceId = component.Id;
                }
//                
//                labFacelessOpData.EffectiveValue = component.EffectiveValue;
//                labFacelessOpData.EffectiveValueSourceId = component.Id;

                if (!string.IsNullOrWhiteSpace(component.Comment)) labFacelessOpData.Comment += component.Comment + " ";

                // Exit when OpData model got all values
//                if (labFacelessOpData.ImpurityValueSourceId != null
//                    && labFacelessOpData.HumidityValueSourceId != null
//                    && labFacelessOpData.InfectionedValueSourceId != null
//                    && labFacelessOpData.EffectiveValueSourceId != null)
//                    break;
            }

            _opDataRepository.Update<LabFacelessOpData, Guid>(labFacelessOpData);
        }

        private void UnbindTrays(long ticketContainerId)
        {
            // Unbind trays
            var unbindRfidList = _context.Cards.Where(e =>
                                     e.TicketContainerId == ticketContainerId
                                     && e.TypeId == Dom.Card.Type.LaboratoryTray)
                                 .Select(e => e.Id)
                                 .ToList();

            foreach (var rfid in unbindRfidList)
            {
                var tmpCard = _context.Cards.First(x => x.Id == rfid);
                tmpCard.TicketContainerId = null;
                _context.SaveChanges();
            }
        }

        private static string GenerateComponentComment(LabFacelessOpDataComponent component)
        {
            var result = string.Empty;

            if (component.EffectiveValue.HasValue) result += $" Масличність={component.EffectiveValue.Value},";
            if (component.HumidityValue.HasValue) result += $" Вологість={component.HumidityValue.Value},";
            if (component.ImpurityValue.HasValue) result += $" Смітна дом={component.ImpurityValue.Value},";

            if (component.Comment != null) result += $" Комментар: {component.Comment},";

            return result;
        }
    }
}