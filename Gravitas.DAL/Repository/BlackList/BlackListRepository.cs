using System;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Model.DomainModel.BlackList.TDO;

namespace Gravitas.DAL.Repository.BlackList
{
    public class BlackListRepository : BaseRepository, IBlackListRepository
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
                Drivers = GetQuery<DriversBlackListRecord, int>(),
                Partners = GetQuery<PartnersBlackListRecord, int>(),
                Trailers = GetQuery<TrailersBlackListRecord, int>(),
                Transport = GetQuery<TransportBlackListRecord, int>()
            };
        }

        public void AddPartner(PartnersBlackListRecord partnerRecord)
        {
            if (GetSingleOrDefault<PartnersBlackListRecord, int>(t => t.PartnerId == partnerRecord.PartnerId) == null)
                Add<PartnersBlackListRecord, int>(new PartnersBlackListRecord
                {
                    PartnerId = partnerRecord.PartnerId, Comment = partnerRecord.Comment
                });
        }

        public void DeletePartner(Guid partnerId)
        {
            var record = GetSingleOrDefault<PartnersBlackListRecord, int>(t => t.Partner.Id == partnerId);
            if (record != null)
                Delete<PartnersBlackListRecord, int>(record);
        }
    }
}