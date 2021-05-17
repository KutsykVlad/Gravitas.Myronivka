using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public class OrganizationUnitRepository : BaseRepository<GravitasDbContext>, IOrganizationUnitRepository
    {
        private readonly GravitasDbContext _context;

        public OrganizationUnitRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public OrganizationUnitDetail GetOrganizationUnitDetail(long id)
        {
            var u = _context.OrganizationUnits.AsNoTracking().First(x=> x.Id == id);
            return new OrganizationUnitDetail
            {
                Id = u.Id,
                Name = u.Name,
                UnitTypeId = u.UnitType.Id,
                UnitTypeName = u.UnitType.Name,
                ChildItems = null,
                NodeItems = new NodeItems
                {
                    Items = u.NodeSet.Select(e => new NodeItem
                    {
                        Id = e.Id, Name = e.Name
                    }).ToList(),
                    Count = u.NodeSet.Count
                }
            };
        }
    }
}