using System.Collections.Generic;
using System.Linq;

namespace Gravitas.Model.Dto
{
    public class BlackListDto
    {
        public IQueryable<DriversBlackListRecord> Drivers { get; set; }
        public IQueryable<PartnersBlackListRecord> Partners { get; set; }
        public IQueryable<TrailersBlackListRecord> Trailers { get; set; }
        public IQueryable<TransportBlackListRecord> Transport { get; set; }

    }
}
