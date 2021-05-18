using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.PackingTare.DAO;
using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.DAL.Repository.PackingTare
{
    public class PackingTareRepository : BaseRepository, IPackingTareRepository
    {
        private readonly GravitasDbContext _context;

        public PackingTareRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<PackingTareVm> GetTareList()
        {
            var list = GetQuery<Model.DomainModel.PackingTare.DAO.PackingTare, int>()
                .Select(x => new PackingTareVm
                {
                    Id = x.Id, 
                    Title = x.Title,
                    Weight = x.Weight
                });
            return list;
        }

        public IQueryable<TicketPackingTareVm> GetTicketTareList(int ticketId)
        {
            var list = GetQuery<TicketPackingTare, int>()
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
            var item = GetFirstOrDefault<Model.DomainModel.PackingTare.DAO.PackingTare, int>(x => 
                x.Title == model.Title);

            if (item != null) return false;
            
            AddOrUpdate<Model.DomainModel.PackingTare.DAO.PackingTare, int>(new Model.DomainModel.PackingTare.DAO.PackingTare
            { 
                Title = model.Title,
                Weight = model.Weight
            });
            return true;
        }
        
        public void Remove(int id)
        {
            var tare = _context.PackingTares.FirstOrDefault(x => x.Id == id);
            if (tare != null)
            {
                Delete<Model.DomainModel.PackingTare.DAO.PackingTare, int>(tare);
            }
        }

        public void RemoveTicketTare(int ticketId)
        {
            foreach (var ticketTare in _context.TicketPackingTares.Where(x => x.TicketId == ticketId).ToList())
            {
                Delete<TicketPackingTare, int>(ticketTare);
            }
        }

        public float GetTareWeight(int tareId)
        {
            return _context.PackingTares.First(x => x.Id == tareId).Weight;
        }

        public bool AddTicketTare(int ticketId, int tareId, int count)
        {
            var item = new TicketPackingTare
            {
                TicketId = ticketId, PackingTareId = tareId, Count = count
            };

            Add<TicketPackingTare, int>(item);

            return true;
        }
    }
}