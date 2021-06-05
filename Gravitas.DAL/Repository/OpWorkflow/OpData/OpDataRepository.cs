using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.OpData.DAO.Base;
using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainValue;
using SingleWindowOpData = Gravitas.Model.DomainModel.OpData.DAO.SingleWindowOpData;

namespace Gravitas.DAL.Repository.OpWorkflow.OpData
{
    public class OpDataRepository : BaseRepository, IOpDataRepository
    {
        private readonly GravitasDbContext _context;
        public OpDataRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }
        
        public ICollection<BaseOpData> GetOpDataList(int ticketId)
        {
            var dao = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (dao == null) return null;

            return new List<BaseOpData>()
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
                .Union(dao.NonStandartOpDataSet)
                .OrderBy(e => e.CheckInDateTime)
                .ToList();
        }

        public BaseOpData GetLastOpData(int? ticketId, OpDataState? stateId = null)
        {
            if (ticketId == null)
                return null;
            return stateId == null
                ? GetOpDataList(ticketId.Value).LastOrDefault()
                : GetOpDataList(ticketId.Value).LastOrDefault(e => e.StateId == stateId);
        }

        public TEntity GetLastProcessed<TEntity>(int? ticketId) where TEntity : BaseOpData
        {
            if (ticketId == null)
                return null;
            return (from optData in _context.Set<TEntity>().AsNoTracking()
                    where optData.TicketId == ticketId && optData.StateId == OpDataState.Processed
                    orderby optData.CheckInDateTime descending
                    select optData).FirstOrDefault();
        }

        public TEntity GetLastProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData
        {
            return (from item in _context.Set<TEntity>().AsNoTracking().Where(predicate)
                    where item.StateId == OpDataState.Processed
                    orderby item.CheckInDateTime descending
                    select item).FirstOrDefault();
        }

        public TEntity GetFirstProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData
        {
            return (from item in _context.Set<TEntity>().AsNoTracking().Where(predicate)
                    where item.StateId == OpDataState.Processed
                    orderby item.CheckInDateTime
                    select item).FirstOrDefault();
        }

        public TEntity GetLastOpData<TEntity>(int? ticketId, OpDataState? stateId) where TEntity : BaseOpData
        {
            if (ticketId == null)
                return null;

            return stateId == null
                ? (from item in _context.Set<TEntity>().AsNoTracking()
                    where item.TicketId == ticketId
                    orderby item.CheckInDateTime descending
                    select item).FirstOrDefault()

                : (from item in _context.Set<TEntity>().AsNoTracking()
                    where item.TicketId == ticketId && item.StateId == stateId
                    orderby item.CheckInDateTime descending
                    select item).FirstOrDefault();
        }

        public void SetSingleWindowDetail(SingleWindowOpDataDetail dto)
        {
            var dao = _context.SingleWindowOpDatas.First(x => x.Id == dto.Id);

            dao.LoadTargetDeviationMinus = dto.LoadTargetDeviationMinus;
            dao.LoadTargetDeviationPlus = dto.LoadTargetDeviationPlus;
            dao.ContactPhoneNo = dto.ContactPhoneNo;
            dao.LoadTarget = dto.LoadTarget;
            dao.CreateDate = dto.CreateDate;
            dao.EditDate = dto.EditDate;
            dao.DriverOneId = dto.DriverOneId;
            dao.DriverTwoId = dto.DriverTwoId;
            dao.TransportId = dto.TransportId;
            dao.HiredDriverCode = dto.HiredDriverCode;
            dao.HiredTransportNumber = dto.HiredTransportNumber;
            dao.IncomeInvoiceSeries = dto.IncomeInvoiceSeries;
            dao.IncomeInvoiceNumber = dto.IncomeInvoiceNumber;
            dao.ReceiverDepotId = dto.ReceiverDepotId;
            dao.IsThirdPartyCarrier = dto.IsThirdPartyCarrier;
            dao.CarrierCode = dto.CarrierCode;
            dao.BuyBudgetId = dto.BuyBudgetId;
            dao.SellBudgetId = dto.SellBudgetId;
            dao.PackingWeightValue = dto.PackingWeightValue;
            dao.DocHumidityValue = dto.DocHumidityValue;
            dao.DocImpurityValue = dto.DocImpurityValue;
            dao.DocNetValue = dto.DocNetValue;
            dao.DocNetDateTime = dto.DocNetDateTime;
            dao.TrailerId = dto.TrailerId;
            dao.HiredTrailerNumber = dto.TrailerNumber;
            dao.TripTicketNumber = dto.TripTicketNumber;
            dao.TripTicketDateTime = dto.TripTicketDateTime;
            dao.WarrantSeries = dto.WarrantSeries;
            dao.WarrantNumber = dto.WarrantNumber;
            dao.WarrantDateTime = dto.WarrantDateTime;
            dao.WarrantManagerName = dto.WarrantManagerName;
            dao.RuleNumber = dto.RuleNumber;
            dao.IncomeDocGrossValue = dto.IncomeDocGrossValue;
            dao.IncomeDocTareValue = dto.IncomeDocTareValue;
            dao.IncomeDocDateTime = dto.IncomeDocDateTime;
            dao.Comments = dto.Comments;
            dao.StampList = dto.StampList;
            dao.SupplyTransportTypeId = dto.SupplyTransportTypeId;
            dao.BatchNumber = dto.BatchNumber;
            dao.InformationCarrier = dto.InformationCarrier;
            dao.CustomPartnerName = dto.CustomPartnerName;
            dao.CarrierId = dto.CarrierId;
            dao.OrganizationTitle = dto.OrganizationName;
            dao.ReceiverTitle = dto.ReceiverName;
            dao.ProductTitle = dto.ProductName;

            Update<SingleWindowOpData, Guid>(dao);
        }
    }
}