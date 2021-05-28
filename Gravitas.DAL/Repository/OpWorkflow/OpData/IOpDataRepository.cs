using System;
using System.Collections.Generic;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.OpData.DAO.Base;
using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainModel.OpDataState.DTO.Detail;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.OpWorkflow.OpData
{
    public interface IOpDataRepository : IBaseRepository
    {
        ICollection<BaseOpData> GetOpDataList(long ticketId);
        BaseOpData GetLastOpData(long? ticketId, OpDataState? stateId = null);


        TEntity GetLastProcessed<TEntity>(long? ticketId) where TEntity : BaseOpData;
        TEntity GetLastProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData;

        TEntity GetFirstProcessed<TEntity>(long? ticketId) where TEntity : BaseOpData;
        TEntity GetFirstProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData;

        TEntity GetLastOpData<TEntity>(long? ticketId, OpDataState? stateId) where TEntity : BaseOpData;

        void SetSingleWindowDetail(SingleWindowOpDataDetail dto);
    }
}