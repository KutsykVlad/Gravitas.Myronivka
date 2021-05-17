using System.Linq;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.Model.DomainModel.BlackList.TDO
{
    public class BlackListDto
    {
        public IQueryable<DriversBlackListRecord> Drivers { get; set; }
        public IQueryable<PartnersBlackListRecord> Partners { get; set; }
        public IQueryable<TrailersBlackListRecord> Trailers { get; set; }
        public IQueryable<TransportBlackListRecord> Transport { get; set; }

    }
}
