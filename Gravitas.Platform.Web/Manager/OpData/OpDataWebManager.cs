using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpData.DAO.Base;
using Gravitas.Model.DomainModel.OpData.TDO.Json;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.Models;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.NonStandard;
using Gravitas.Platform.Web.ViewModel.OpData.NonStandart;
using Microsoft.Ajax.Utilities;
using Dom = Gravitas.Model.DomainValue.Dom;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.DAO.LabFacelessOpData;
using LabFacelessOpDataComponent = Gravitas.Model.DomainModel.OpData.TDO.Detail.LabFacelessOpDataComponent;

namespace Gravitas.Platform.Web.Manager.OpData
{
    public class OpDataWebManager : IOpDataWebManager
    {
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly INodeManager _nodeManager;
        private readonly IOpDataRepository _opDataRepository;
        private readonly GravitasDbContext _context;

        private Dictionary<long, string> States = new Dictionary<long, string>
        {
            {
                Dom.OpDataState.Init, "Бланк"
            },
            {
                Dom.OpDataState.Processing, "В обробці"
            },
            {
                Dom.OpDataState.Collision, "На погодженні"
            },
            {
                Dom.OpDataState.CollisionApproved, "Погодженно"
            },
            {
                Dom.OpDataState.CollisionDisapproved, "Відмовлено у погодженні"
            },
            {
                Dom.OpDataState.Rejected, "Відмовлено"
            },
            {
                Dom.OpDataState.Canceled, "Скасовано"
            },
            {
                Dom.OpDataState.Processed, "Виконано"
            },
            {
                Dom.OpDataState.PartLoad, "Часткове завантаження"
            },
            {
                Dom.OpDataState.PartUnload, "Часткове розвантаження"
            },
            {
                Dom.OpDataState.Reload, "Перезавантаження"
            }
        };

        public OpDataWebManager(IOpDataRepository opDataRepository,
            INodeManager nodeManager,
            IExternalDataRepository externalDataRepository,
            GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _nodeManager = nodeManager;
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        public OpDataItemsVm GetOpDataItems(long ticketId)
        {
            var dao = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            var tt = new List<BaseOpData>()
                .Union(dao.LabFacelessOpDataSet)
                .Union(dao.SingleWindowOpDataSet)
                .Union(dao.SecurityCheckInOpDataSet)
                .Union(dao.SecurityCheckOutOpDataSet)
                .Union(dao.ScaleOpDataSet)
                .Union(dao.UnloadPointOpDataSet)
                .Union(dao.LoadPointOpDataSet)
                .Union(dao.LoadGuideOpDataSet)
                .Union(dao.UnloadGuideOpDataSet)
                .Union(dao.CentralLabOpDataSet)
                .Union(dao.MixedFeedGuideOpDataSet)
                .Union(dao.MixedFeedLoadOpDataSet)
                .Union(dao.NonStandartOpDataSet)
                .ToList();

            var result = tt
                .Select(item => new OpDataItemVm
                {
                    Id = item.Id,
                    OpDataType = new
                    {
                        item.Node.Id,
                        item.Node.Name
                    },
                    CheckInDateTime = item.CheckInDateTime,
                    CheckOutDateTime = item.CheckOutDateTime,
                    StateId = item.StateId,
                    StateName = States[item.StateId],
                    NodeId = item.NodeId,
                    NodeName = item.Node.Name,
                    NodeCode = item.Node.Code,
                    Message = GetOpDataItemMessage(item),
                    IsNonStandard = item is NonStandartOpData,
                    OpVisaCount = item.OpVisaSet.Count,
                    OpCameraImageCount = item.OpCameraSet.Count,
                    Signatures = item.OpVisaSet.Select(t => new OpDataItemSignature
                    {
                        ExternalUserName = t.Employee?.FullName ?? t.Employee?.ShortName ?? string.Empty,
                        State = t.OpRoutineState.Name,
                        Time = t.DateTime
                    }).ToList()
                })
                .OrderBy(e => e.CheckOutDateTime)
                .ToList();


            return new OpDataItemsVm
            {
                Count = result.Count,
                Items = result
            };
        }

        public NonStandartRegistryItemsVm GetNonStandardRegistryItems(NonStandartDataFilters filters, int pageNumber = 1, int pageSize = 25)
        {
            var list = _context.NonStandartOpDatas
                .AsNoTracking()
                .Where(item => (filters.NodeId == null || filters.NodeId == item.NodeId) &&
                               (filters.LeftScope == null || filters.LeftScope <= item.CheckInDateTime) &&
                               (filters.RightScope == null || filters.RightScope >= item.CheckInDateTime))
                .ToList();

            var lastPage = (list.Count - 1) / pageSize + 1;

            var nodes = list
                .DistinctBy(item => item.NodeId)
                .Select(item => new NodeInfo
                {
                    NodeName = item.Node.Name,
                    NodeId = item.NodeId
                })
                .ToList();

            var beginDate = filters.LeftScope;
            var endDate = filters.RightScope;
            var result = list
                .OrderBy(item => item.CheckInDateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(item => new NonStandartRegistryItemVm
                {
                    Id = item.Id,
                    OpDataType = new
                    {
                        item.Node.Id,
                        item.Node.Name
                    },
                    CheckInDateTime = item.CheckInDateTime,
                    CheckOutDateTime = item.CheckOutDateTime,
                    StateId = item.StateId,
                    NodeId = item.NodeId,
                    NodeName = item.Node.Name,
                    Message = item.Message,
                    TruckBaseData = item.TicketId.HasValue ? GetTruckBaseData(item.TicketId.Value) : null
                })
                .ToList();

            return new NonStandartRegistryItemsVm
            {
                Count = result.Count,
                Items = result,
                PrevPage = Math.Max(pageNumber - 1, 1),
                NextPage = Math.Min(pageNumber + 1, lastPage),
                BeginDate = filters.LeftScope ?? beginDate,
                EndDate = filters.RightScope ?? endDate,
                RelatedNodeId = filters.NodeId,
                Nodes = nodes
            };
        }

        public OpCameraImageItemsVm GetOpCameraImageItems(Guid opDataId)
        {
            var imageList = _context.OpCameraImages.Where(e =>
                e.SecurityCheckInOpDataId == opDataId
                || e.LabFacelessOpDataId == opDataId
                || e.LabRegularOpDataId == opDataId
                || e.LoadPointOpDataId == opDataId
                || e.SingleWindowOpDataId == opDataId
                || e.SecurityCheckInOpDataId == opDataId
                || e.SecurityCheckOutOpDataId == opDataId
                || e.ScaleOpDataId == opDataId
                || e.UnloadPointOpDataId == opDataId
                || e.NonStandartOpDataId == opDataId
            ).Select(e => new OpCameraImageItemVm
            {
                Id = e.Id,
                DateTime = e.DateTime,
                ImagePath = e.ImagePath,
                SourceDeviceName = e.Device.Name
            }).ToList();

            var result = new OpCameraImageItemsVm
            {
                Items = imageList,
                Count = imageList.Count
            };
            return result;
        }

        public LaboratoryInVms.LabFacelessOpDataComponentItemsVm LaboratoryIn_GetListComponentItems(Guid opDataId)
        {
            var dao = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == opDataId);
            if (dao == null) return null;

            // TODO: Move to repository
            var dto = new Model.DomainModel.OpData.TDO.Detail.LabFacelessOpData
            {
                Id = dao.Id,
                TicketContainerId = dao.TicketContainerId,
                TicketId = dao.TicketId,
                NodeId = dao.NodeId,
                StateId = dao.StateId,
                CheckInDateTime = dao.CheckInDateTime,
                CheckOutDateTime = dao.CheckOutDateTime,
                ImpurityClassId = dao.ImpurityClassId,
                ImpurityValue = dao.ImpurityValue,
                HumidityClassId = dao.HumidityClassId,
                HumidityValue = dao.HumidityValue,
                InfectionedClassId = dao.InfectionedClassId,
                EffectiveValue = dao.EffectiveValue,
                Comment = dao.Comment,
                DataSourceName = dao.DataSourceName,
                LabFacelessOpDataItemSet = dao.LabFacelessOpDataComponentSet.Select(e => new LabFacelessOpDataComponent
                {
                    Id = e.Id,
                    StateId = e.StateId,
                    LabFacelessOpDataId = e.LabFacelessOpDataId,
                    ImpurityClassId = e.ImpurityClassId,
                    ImpurityValue = e.ImpurityValue,
                    HumidityClassId = e.HumidityClassId,
                    HumidityValue = e.HumidityValue,
                    InfectionedClassId = e.InfectionedClassId,
                    EffectiveValue = e.EffectiveValue,
                    Comment = e.Comment,
                    AnalysisTrayRfid = e.AnalysisTrayRfid,
                    AnalysisValueDescriptor = AnalysisValueDescriptor.FromJson(e.AnalysisValueDescriptor)
                }).ToList()
            };

            var vm = new LaboratoryInVms.LabFacelessOpDataComponentItemsVm
            {
                Items = dto.LabFacelessOpDataItemSet.Select(e => new LaboratoryInVms.LabFacelessOpDataComponentItemVm
                {
                    Id = e.Id,
                    LabFacelessOpDataId = e.LabFacelessOpDataId,
                    StateId = e.StateId,
                    StateName = States[e.StateId],
                    AnalysisTrayRfid = e.AnalysisTrayRfid,
                    AnalysisValueDescriptor = Mapper.Map<LaboratoryInVms.AnalysisValueDescriptorVm>(e.AnalysisValueDescriptor),
                    ImpurityClassName = _context.LabImpurityСlassifiers.FirstOrDefault(x => x.Id == e.ImpurityClassId)?.Name,
                    ImpurityValue = e.ImpurityValue,
                    HumidityClassName = _context.LabHumidityСlassifiers.FirstOrDefault(x => x.Id == e.HumidityClassId)?.Name,
                    HumidityValue = e.HumidityValue,
                    IsInfectionedClassId = e.InfectionedClassId,
                    EffectiveValue = e.EffectiveValue,
                    Comment = e.Comment,
                    DataSourceName = e.DataSourceName
                }).ToList()
            };

            return vm;
        }

        private TruckBaseData GetTruckBaseData(long ticketId)
        {
            var result = new TruckBaseData();
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) return result;

            var singleWindowOpData = _opDataRepository.GetLastOpData<SingleWindowOpData>(ticket.Id, null);
            if (singleWindowOpData != null)
            {
                result.IncomeInvoiceNumber = singleWindowOpData.IncomeInvoiceNumber;
                result.Product = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ??
                                 string.Empty;

                result.Carrier = !string.IsNullOrWhiteSpace(singleWindowOpData.CarrierId)
                    ? _externalDataRepository.GetPartnerDetail(singleWindowOpData.CarrierId)?.ShortName
                    : singleWindowOpData.CustomPartnerName ?? string.Empty;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    result.TruckNo = singleWindowOpData.HiredTransportNumber;
                    result.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                    result.Driver1 = singleWindowOpData.HiredDriverCode;
                }
                else
                {
                    result.TruckNo = _context.FixedAssets.FirstOrDefault(x => x.Id == singleWindowOpData.TransportId)?.RegistrationNo;
                    result.TrailerNo = _context.FixedAssets.FirstOrDefault(x => x.Id == singleWindowOpData.TrailerId)?.RegistrationNo;
                    result.Driver1 = _context.Employees.FirstOrDefault(x => x.Id == singleWindowOpData.DriverOneId)?.ShortName;
                    result.Driver2 = _context.Employees.FirstOrDefault(x => x.Id == singleWindowOpData.DriverTwoId)?.ShortName;
                }
            }

            return result;
        }

        private string GetOpDataItemMessage(BaseOpData opData)
        {
            switch (opData)
            {
                case SingleWindowOpData singleWindowOpData:
                    return singleWindowOpData.SupplyCode;
                case LabFacelessOpData labFacelessOpData:
                    return $"В:{labFacelessOpData.HumidityValue} З:{labFacelessOpData.ImpurityValue} П/М:{labFacelessOpData.EffectiveValue}\n" +
                           $"{labFacelessOpData.OpVisaSet.FirstOrDefault(e => e.OpRoutineStateId == Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage)?.Message}";
                case ScaleOpData scaleOpData:
                    return
                        $" {(scaleOpData.TypeId == Dom.ScaleOpData.Type.Tare ? "Tара" : "Брутто")}: {scaleOpData.TruckWeightValue ?? 0 + scaleOpData.TrailerWeightValue ?? 0}";
                case NonStandartOpData nonStandartOpData:
                    return nonStandartOpData.Message;
                case LoadGuideOpData loadGuideOpData:
                    return _nodeManager.GetNodeName(loadGuideOpData.LoadPointNodeId);
                case UnloadGuideOpData unloadGuideOpData:
                    return _nodeManager.GetNodeName(unloadGuideOpData.UnloadPointNodeId);
                case MixedFeedGuideOpData mixedFeedGuideOpData:
                    return _nodeManager.GetNodeName(mixedFeedGuideOpData.LoadPointNodeId);
                case CentralLabOpData centralLabOpData:
                    var labComment = string.IsNullOrWhiteSpace(centralLabOpData.LaboratoryComment)
                        ? null
                        : $"Коментар лаборанта: {centralLabOpData.LaboratoryComment}";
                    var managerComment = string.IsNullOrWhiteSpace(centralLabOpData.CollisionComment)
                        ? null
                        : $"\nКоментар майстра: {centralLabOpData.CollisionComment}";
                    return $"{labComment} {managerComment}";
                case UnloadPointOpData unloadPointOpData:
                    return unloadPointOpData.Valve;
                default:
                    return string.Empty;
            }
        }
    }
}