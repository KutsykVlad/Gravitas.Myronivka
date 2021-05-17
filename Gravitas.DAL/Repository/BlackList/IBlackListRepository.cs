using Gravitas.Model;

namespace Gravitas.DAL.Repository
{
    public interface IBlackListRepository : IBaseRepository<GravitasDbContext>
    {
        Model.Dto.BlackListDto GetBlackListDto();

        void AddPartner(PartnersBlackListRecord partnerRecord);
        void DeletePartner(string partnerId);

    }
}
