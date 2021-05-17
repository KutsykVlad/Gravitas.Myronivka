using Gravitas.Model;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Model.DomainModel.BlackList.TDO;

namespace Gravitas.DAL.Repository
{
    public interface IBlackListRepository : IBaseRepository<GravitasDbContext>
    {
        BlackListDto GetBlackListDto();

        void AddPartner(PartnersBlackListRecord partnerRecord);
        void DeletePartner(string partnerId);

    }
}
