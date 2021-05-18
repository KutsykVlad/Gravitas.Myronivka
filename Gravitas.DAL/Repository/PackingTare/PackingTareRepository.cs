using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.PackingTare.DAO;
using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.DAL.Repository.PackingTare
{
    public class PackingTareRepository : BaseRepository<GravitasDbContext>, IPackingTareRepository
    {
        private readonly GravitasDbContext _context;

        public PackingTareRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<PackingTareVm> GetTareList()
        {
            var list = GetQuery<Model.DomainModel.PackingTare.DAO.PackingTare, long>()
                .Select(x => new PackingTareVm
                {
                    Id = x.Id, 
                    Title = x.Title,
                    Weight = x.Weight
                });
            return list;
        }

        public IQueryable<TicketPackingTareVm> GetTicketTareList(long ticketId)
        {
            var list = GetQuery<TicketPackingTare, long>()
                .Where(x => x.TicketId == ticketId)
                .Select(x => new TicketPackingTareVm
                {
                    PackingTitle = x.PackingTare.Title,
                    Count = x.Count,
                    PackingId = x.PackingTareId,
                    TicketId = x.TicketId
                });
            return list;
        }

        public bool Add(PackingTareVm model)
        {
            var item = GetFirstOrDefault<Model.DomainModel.PackingTare.DAO.PackingTare, long>(x => 
                x.Title == model.Title);

            if (item != null) return false;
            
            AddOrUpdate<Model.DomainModel.PackingTare.DAO.PackingTare, long>(new Model.DomainModel.PackingTare.DAO.PackingTare
            { 
                Title = model.Title,
                Weight = model.Weight
            });
            return true;
        }
        
        public void Remove(long id)
        {
            var tare = GetEntity<Model.DomainModel.PackingTare.DAO.PackingTare, long>(id);
            if (tare != null)
            {
                Delete<Model.DomainModel.PackingTare.DAO.PackingTare, long>(tare);
            }
        }

        public void RemoveTicketTare(long ticketId)
        {
            foreach (var ticketTare in _context.TicketPackingTares.Where(x => x.TicketId == ticketId).ToList())
            {
                Delete<TicketPackingTare, long>(ticketTare);
            }
        }

        public float GetTareWeight(long tareId)
        {
            return GetEntity<Model.DomainModel.PackingTare.DAO.PackingTare, long>(tareId).Weight;
        }

        public bool AddTicketTare(long ticketId, long tareId, int count)
        {
            var item = new TicketPackingTare
            {
                TicketId = ticketId, PackingTareId = tareId, Count = count
            };

            Add<TicketPackingTare, long>(item);

            return true;
        }
    }
}