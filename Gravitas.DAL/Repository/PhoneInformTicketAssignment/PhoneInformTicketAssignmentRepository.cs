using System.Data.Entity;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;

namespace Gravitas.DAL.Repository.PhoneInformTicketAssignment
{
    public class PhoneInformTicketAssignmentRepository : BaseRepository, IPhoneInformTicketAssignmentRepository
    {
        private readonly GravitasDbContext _context;

        public PhoneInformTicketAssignmentRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public void Add(Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment model)
        {
            Add<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, int>(model);
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            Delete<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, int>(item);
        }

        public IQueryable<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment> GetAll()
        {
            return GetQuery<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, int>()
                .Include("PhoneDictionary");
        }

        public Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment GetById(int id)
        {
            return _context.PhoneInformTicketAssignments.FirstOrDefault(x => x.Id == id);
        }
    }
}
