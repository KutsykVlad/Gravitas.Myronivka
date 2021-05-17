using Gravitas.Model;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Model.DomainModel.BlackList.TDO;
using Gravitas.Model.Dto;

namespace Gravitas.DAL.Repository
{
    public class BlackListRepository : BaseRepository<GravitasDbContext>, IBlackListRepository
    {
        private readonly GravitasDbContext _context;
        public BlackListRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public BlackListDto GetBlackListDto()
        {
            return new BlackListDto
            {
                Drivers = GetQuery<DriversBlackListRecord, long>(),
                Partners = GetQuery<PartnersBlackListRecord, long>(),
                Trailers = GetQuery<TrailersBlackListRecord, long>(),
                Transport = GetQuery<TransportBlackListRecord, long>()
            };
        }

        public void AddPartner(PartnersBlackListRecord partnerRecord)
        {
            if (GetSingleOrDefault<PartnersBlackListRecord, long>(t => t.PartnerId == partnerRecord.PartnerId) == null)
                Add<PartnersBlackListRecord, long>(new PartnersBlackListRecord
                {
                    PartnerId = partnerRecord.PartnerId, Comment = partnerRecord.Comment
                });
        }

        public void DeletePartner(string partnerId)
        {
            var record = GetSingleOrDefault<PartnersBlackListRecord, long>(t => t.Partner.Id == partnerId);
            if (record != null)
                Delete<PartnersBlackListRecord, long>(record);
        }
    }
}