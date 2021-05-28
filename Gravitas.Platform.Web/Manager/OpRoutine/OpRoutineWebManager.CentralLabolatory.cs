using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;
using Microsoft.Ajax.Utilities;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public CentralLaboratoryProcess.IdleVm CentralLaboratoryProcess_Idle_GetVm(int nodeId)
        {
            return new CentralLaboratoryProcess.IdleVm
            {
                NodeId = nodeId
            };
        }

        public void CentralLaboratoryProcess_Idle_SelectSample(int nodeId, Guid opDataId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto  == null) return;

            var opData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == opDataId);
            if (opData == null) return;
            nodeDto.Context.TicketId = opData.TicketId;
            nodeDto.Context.OpDataId = opDataId;
            nodeDto.Context.TicketContainerId = opData.TicketContainerId;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public CentralLaboratoryProcess.PrintDataDiscloseVm CentralLaboratoryProcess_PrintDataDisclose_GetVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpDataId == null) return null;
            var opDataDetail = GetCentralLabOpDataDetailVm(nodeDto.Context.OpDataId.Value);
            
            var opData = _context.CentralLabOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            var collisionApprovalMessage = opData.OpVisaSet
                .Where(x => x.OpRoutineStateId == Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit)
                .Select(e => e.Message)
                .LastOrDefault();
    
            var vm = new CentralLaboratoryProcess.PrintDataDiscloseVm
            {
                NodeId = nodeId,
                OpDataDetail = opDataDetail,
                IsLabFile = _ticketRepository.GetTicketFilesByType(TicketFileType.CentralLabCertificate).Any(item => item.TicketId == opDataDetail.TicketId),
                IsCollisionMode = opDataDetail.LabComment != null,
                CollisionApprovalMessage = collisionApprovalMessage
            };

            return vm;
        }

        public void CentralLaboratoryProcess_PrintCollisionInit_Send(int nodeId, string comment)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return;

            var opData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;
            opData.LaboratoryComment = comment;
            _opDataRepository.Update<CentralLabOpData, Guid>(opData);
          
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void CentralLaboratoryProcess_PrintDataDisclose_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void CentralLaboratoryProcess_Idle_AddSample(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.AddSample;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);

        }

        public void CentralLaboratoryProcess_Idle_AddSample_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool CentralLaboratoryProcess_PrintDataDisclose_Confirm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return false;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public CentralLaboratoryProcess.PrintCollisionInitVm CentralLaboratory_GetCollisionInitVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null || nodeDto.Context.OpDataId == null) return null;

            return new CentralLaboratoryProcess.PrintCollisionInitVm
            {
                NodeId = nodeId,
                TicketId = nodeDto.Context.TicketId.Value,
                ManagerList = _context.EmployeeRoles.Where(x => x.RoleId == (long) UserRole.LaboratoryManager)
                    .AsEnumerable()
                     .Select(l => _context.Employees.First(x => x.Id == l.EmployeeId))
                     .OrderBy(x => x.ShortName)
                     .ToDictionary(x => x.Id)
            };
        }

        public CentralLaboratoryProcess.PrintDocumentVm CentralLaboratory_GetPrintDocumentVm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null 
                || nodeDto?.Context.OpDataId == null) return null;

            return new CentralLaboratoryProcess.PrintDocumentVm
            {
                NodeId = nodeId,
                TicketId = nodeDto.Context.TicketId.Value,
                OpDataId = nodeDto.Context.OpDataId.Value
            };
        }

        public CentralLaboratoryProcess.CentralLaboratoryLabelVm CentralLaboratory_GetLabelPrintoutVm(Guid id)
        {
            var vm = new CentralLaboratoryProcess.CentralLaboratoryLabelVm();

            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == id);

            if (centralLabOpData == null) return null;

            vm.CheckOutDateTime = centralLabOpData.SampleCheckOutTime;

            vm.SampleCollectEmployee =
                centralLabOpData.OpVisaSet
                                .Where(x => x.OpRoutineStateId == Model.DomainValue.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa)
                                .OrderByDescending(x => x.DateTime)
                                .FirstOrDefault()?
                                .Employee?.ShortName ?? string.Empty;
            vm.LabProcessEmployee =
                centralLabOpData.OpVisaSet
                                .Where(x => x.OpRoutineStateId == Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa)
                                .OrderByDescending(x => x.DateTime)
                                .FirstOrDefault()?
                                .Employee?.ShortName ?? string.Empty;

            if (centralLabOpData.TicketId.HasValue)
            {
                var data = _opDataManager.GetBasicTicketData(centralLabOpData.TicketId.Value);
                vm.ProductName = data.ProductName;
                vm.SenderName = data.SenderName;
                vm.TransportNo = data.TransportNo;
                vm.TrailerNo = data.TrailerNo;
            }
           

            return vm;
        }

        public void CentralLaboratoryProcess_PrintDataDisclose_DeleteFile(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null) return;

            var ticketFile = _opDataRepository.GetFirstOrDefault<TicketFile, int>(x =>
                x.TicketId == nodeDto.Context.TicketId 
                && x.TypeId == TicketFileType.CentralLabCertificate);
            
            if (ticketFile != null) _opDataRepository.Delete<TicketFile, int>(ticketFile);
            
            SignalRInvoke.ReloadHubGroup(nodeId);
        }

        public void CentralLaboratoryProcess_PrintCollisionVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void CentralLaboratoryProcess_PrintAddOpVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpDataId == null) return;
            
            var opData = _context.CentralLabOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData.StateId == OpDataState.Canceled) opData.LaboratoryComment = null;
            opData.StateId = OpDataState.Processing;
            _opDataRepository.Update<CentralLabOpData, Guid>(opData);

            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }   
        
        public void CentralLaboratoryProcess_PrintDataDiscloseVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpDataId == null) return;
            
            var opData = _context.CentralLabOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            opData.LaboratoryComment = null;
            opData.StateId = OpDataState.Processing;
            _opDataRepository.Update<CentralLabOpData, Guid>(opData);

            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void CentralLaboratoryProcess_PrintLabel_Confirm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool CentralLaboratoryProcess_SendToCollision(CentralLaboratoryProcess.PrintCollisionInitVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context.OpDataId == null) return false;
            _nodeRepository.ClearNodeProcessingMessage(vm.NodeId);

            if (!vm.Phone1.IsNullOrWhiteSpace() && (!vm.Phone1.All(char.IsDigit) || vm.Phone1.Length != 12)
                || !vm.Phone2.IsNullOrWhiteSpace() && (!vm.Phone2.All(char.IsDigit) || vm.Phone1.Length != 12)
                || !vm.Phone3.IsNullOrWhiteSpace() && (!vm.Phone3.All(char.IsDigit) || vm.Phone1.Length != 12))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error, @"Не вірний формат телефонного номеру."));
                return false;
            }

            var opData = _context.CentralLabOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            opData.StateId = Model.DomainValue.OpDataState.Collision;
            opData.CollisionComment = vm.Comment;
            _opDataRepository.Update<CentralLabOpData, Guid>(opData);

            if (!_collisionWebManager.SendToConfirmation(vm.TicketId,
                new List<string> { vm.Phone1, vm.Phone2, vm.Phone3 },
                new List<string> { vm.Email1, vm.Email2, vm.Email3 }, EmailTemplate.CentralLaboratoryCollisionApproval)) return false;

            Logger.Info("Laboratory collision send: Try send driver sms");
            if (!_connectManager.SendSms(SmsTemplate.DriverQualityMatchingSms, nodeDto.Context.TicketId))
                Logger.Error("Message hasn`t been sent");
            else Logger.Info("Laboratory collision send: Driver sms send.");

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle;
            _nodeRepository.ClearNodeProcessingMessage(vm.NodeId);
            return UpdateNodeContext(vm.NodeId, nodeDto.Context);
        }

        public bool CentralLaboratoryProcess_PrintCollisionInit_Return(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool CentralLaboratoryProcess_PrintCollisionManage_ReturnToCollectSamples(int nodeId, string comment)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return false;

            var opData = _context.CentralLabOpDatas.First(x => x.Id ==nodeDto.Context.OpDataId.Value);
            
            opData.StateId = OpDataState.Canceled;
            opData.LaboratoryComment = comment;
            _context.SaveChanges();
           
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa;
            return UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public void CentralLaboratoryProcess_PrintDocument_Confirm(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            UnbindTrays(nodeDto.Context.TicketContainerId.Value);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle; 
            nodeDto.Context.TicketId = null;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpDataId = null;
            _nodeRepository.ClearNodeProcessingMessage(nodeId);
            UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public CentralLaboratoryProcess.CentralLabolatorySamplePrintoutVm CentralLaboratory_GetSamplePrintoutVm(int ticketId)
        {
            var vm = new CentralLaboratoryProcess.CentralLabolatorySamplePrintoutVm();

            var labTicketItems = _context.CentralLabOpDatas.Where(x => x.TicketId == ticketId).ToList();

            var centralLabOpData = labTicketItems.LastOrDefault();
            if (centralLabOpData == null) return vm;
            
            vm.CheckOutDateTime = centralLabOpData.CheckOutDateTime;
            vm.SampleCollectUserName =
                centralLabOpData.OpVisaSet.OrderBy(e => e.DateTime).FirstOrDefault()?.Employee?.ShortName ??
                string.Empty;
            vm.AnalysisReleaseUserName =
                centralLabOpData.OpVisaSet.OrderByDescending(e => e.DateTime).FirstOrDefault()?.Employee?.ShortName ??
                string.Empty;

            if (centralLabOpData.TicketId.HasValue)
            {
                var data = _opDataManager.GetBasicTicketData(centralLabOpData.TicketId.Value);
                vm.ProductName = data.ProductName;
                vm.SenderName = data.SenderName;
                vm.TransportNo = data.TransportNo;
                vm.TrailerNo = data.TrailerNo;
                vm.SingleWindowComment = data.SingleWindowComment;
            }
           
            vm.OpVisaItems = new PrintoutOpVisaItemsVm
            {
                Items = new List<PrintoutOpVisaItemVm>()
            };

            var ticket = _context.Tickets.First(x => x.Id == ticketId);
            var tickets = _context.Tickets.Where(x => x.TicketContainerId == ticket.TicketContainerId && x.StatusId == TicketStatus.Completed).ToList();

            foreach (var item in labTicketItems)
            {
                var opDataItems = item.OpVisaSet
                                      .Select(e => new PrintoutOpVisaItemVm
                                      {
                                          Id = e.Id,
                                          DateTime = e.DateTime,
                                          Message = e.Message,
                                          UserName = _externalDataRepository.GetExternalEmployeeDetail(e.EmployeeId)?.ShortName,
                                          Comment = GenerateLabOpVisaComment(e.OpRoutineStateId, item)
                                      })
                                      .ToList();
                vm.OpVisaItems.Items.AddRange(opDataItems);
            }
            
            foreach (var t in tickets)
            {
                labTicketItems = _context.CentralLabOpDatas.Where(x => x.TicketId == t.Id).ToList();
                foreach (var item in labTicketItems)
                {
                    var opDataItems = item.OpVisaSet
                        .Select(e => new PrintoutOpVisaItemVm
                        {
                            Id = e.Id,
                            DateTime = e.DateTime,
                            Message = e.Message,
                            UserName = _externalDataRepository.GetExternalEmployeeDetail(e.EmployeeId)?.ShortName,
                            Comment = GenerateLabOpVisaComment(e.OpRoutineStateId, item)
                        })
                        .ToList();
                    vm.OpVisaItems.Items.AddRange(opDataItems);
                }
            }

            vm.OpVisaItems.Items = vm.OpVisaItems.Items.OrderBy(e => e.DateTime).ToList();
            return vm;
        }
        
        private string GenerateLabOpVisaComment(int opRoutineStateId, CentralLabOpData centralLabOpData)
        {
            var result = string.Empty;
            switch (opRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa:
                    switch (centralLabOpData.StateId)
                    {
                        case OpDataState.Rejected:
                            var ticket = _context.Tickets.First(x => x.Id == centralLabOpData.TicketId.Value);
                            var routeType = ticket.RouteType;
                            switch (routeType)
                            {
                                case RouteType.Reload:
                                    result += $"Стан: Відправлений на повторне завантаження";
                                    break;  
                                case RouteType.Move:
                                    result += $"Стан: Відправлений на переміщення";
                                    break;
                                    
                            }
                            
                        break; 
                        case OpDataState.Canceled:
                            if (centralLabOpData.OpVisaSet.Any(x =>
                                x.OpRoutineStateId == Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit))
                            {
                                result += $"Стан: Відправлений на перемішення, Коментар: {centralLabOpData.CollisionComment}";  
                            }
                            else
                            {
                                result += $"Стан: Відправлений на повторний відбір проб, Коментар: {centralLabOpData.LaboratoryComment}";  
                            }
                            break; 
                        default:
                            result += $"Стан: Успішно опрацьовано";
                        break;    
                    }
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa:
                    result += $"{centralLabOpData.LaboratoryComment}";
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa:
                    result += $"{centralLabOpData.CollisionComment}";
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit:
                    result += $"{centralLabOpData.LaboratoryComment}, {centralLabOpData.CollisionComment}";
                    break;
            }

            return result;
        }

        public void CentralLaboratoryProcess_PrintDataDisclose_MoveWithLoad(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return;

            var opData = _context.CentralLabOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            opData.CheckOutDateTime = DateTime.Now;
            opData.StateId = OpDataState.Rejected;
            _context.SaveChanges();
            
            nodeDto.Context.OpProcessData = (int?) RouteType.Move;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa;
            _nodeRepository.ClearNodeProcessingMessage(nodeId);
            UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public void CentralLaboratoryProcess_PrintDataDisclose_UnloadToStoreWithLoad(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return;

            var opData = _context.CentralLabOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);
            opData.CheckOutDateTime = DateTime.Now;
            opData.StateId = OpDataState.Rejected;
            _context.SaveChanges();
            
            nodeDto.Context.OpProcessData = (int?) RouteType.Reload;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa;
            _nodeRepository.ClearNodeProcessingMessage(nodeId);
            UpdateNodeContext(nodeId, nodeDto.Context);
        }

        public void CentralLaboratoryProcess_PrintDataDisclose_ToCollisionInit(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa;
            UpdateNodeContext(nodeId, nodeDto.Context);
        }

        private CentralLabVms.CentralLabOpDataOpDataDetailVm GetCentralLabOpDataDetailVm(Guid opDataId)
        {
            var opData = _context.CentralLabOpDatas.First(x => x.Id == opDataId);
            var state = _centralLaboratoryManager.GetTicketStateInCentralLab(opData.TicketId);
            var vm = new CentralLabVms.CentralLabOpDataOpDataDetailVm
            {
                Id = opData.Id,
                NodeId = opData.NodeId,
                TicketId = opData.TicketId,
                CentralLabState = state,
                SampleCheckInDateTime = opData.SampleCheckInDateTime,
                SampleCheckOutDateTime = opData.SampleCheckOutTime,
                CheckOutDateTime = opData.CheckOutDateTime,
                CheckInDateTime = opData.CheckInDateTime,
                State = _centralLaboratoryManager.LabStateName[state],
                IsActive = opData.CheckInDateTime.HasValue,
                CollisionComment = opData.CollisionComment,
                LabComment = opData.LaboratoryComment
            };

            if (opData.TicketId.HasValue)
            {
                var data = _opDataManager.GetBasicTicketData(opData.TicketId.Value);
                vm.ProductName = data.ProductName;
                vm.ReceiverName = data.SenderName;
                vm.TransportNo = data.TransportNo;
                vm.TrailerNo = data.TrailerNo;
            }

            return vm;
        }
    }
}