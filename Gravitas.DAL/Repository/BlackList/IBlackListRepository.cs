using System;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Model.DomainModel.BlackList.TDO;

namespace Gravitas.DAL.Repository.BlackList
{
    public interface IBlackListRepository : IBaseRepository
    {
        BlackListDto GetBlackListDto();

        void AddPartner(PartnersBlackListRecord partnerRecord);
        void DeletePartner(Guid partnerId);

    }
}
