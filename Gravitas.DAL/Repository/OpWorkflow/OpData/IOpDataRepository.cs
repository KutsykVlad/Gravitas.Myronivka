using System;
using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO.Base;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public interface IOpDataRepository : IBaseRepository<GravitasDbContext>
    {
        OpDataStateDetail GetOpDataStateDetail(long id);

        ICollection<BaseOpData> GetOpDataList(long ticketId);
        BaseOpData GetLastOpData(long? ticketId, int? stateId = null);


        TEntity GetLastProcessed<TEntity>(long? ticketId) where TEntity : BaseOpData;
        TEntity GetLastProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData;

        TEntity GetFirstProcessed<TEntity>(long? ticketId) where TEntity : BaseOpData;
        TEntity GetFirstProcessed<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseOpData;

        TEntity GetLastOpData<TEntity>(long? ticketId, int? stateId) where TEntity : BaseOpData;

        void SetSingleWindowDetail(SingleWindowOpDataDetail dto);
    }
}