using System;
using System.Collections.Generic;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.OpData.DAO.Base;
using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.OpWorkflow.OpData
{
    public interface IOpDataRepository : IBaseRepository
    {
        ICollection<BaseOpData> GetOpDataList(int ticketId);
        BaseOpData GetLastOpData(int? ticketId, OpDataState? stateId = null);
        TEntity GetLastProcessed<TEntity>(int? ticketId) where TEntity : BaseOpData;
        TEntity GetLastProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData;
        TEntity GetFirstProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData;
        TEntity GetLastOpData<TEntity>(int? ticketId, OpDataState? stateId) where TEntity : BaseOpData;
        void SetSingleWindowDetail(SingleWindowOpDataDetail dto);
    }
}