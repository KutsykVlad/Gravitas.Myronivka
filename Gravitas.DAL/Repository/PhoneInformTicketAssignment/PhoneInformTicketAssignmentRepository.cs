using System.Data.Entity;
using System.Linq;
using Gravitas.DAL.DbContext;

namespace Gravitas.DAL.Repository.PhoneInformTicketAssignment
{
    public class PhoneInformTicketAssignmentRepository : BaseRepository<GravitasDbContext>, IPhoneInformTicketAssignmentRepository
    {
        private readonly GravitasDbContext _context;

        public PhoneInformTicketAssignmentRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment model)
        {
            Add<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, long>(model);
        }

        public void Delete(long id)
        {
            var item = GetById(id);
            Delete<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, long>(item);
        }

        public IQueryable<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment> GetAll()
        {
            return GetQuery<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, long>()
                .Include("PhoneDictionary");
        }

        public Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment GetById(long id)
        {
            return GetEntity<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, long>(id);
        }
    }
}
