using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.LoadPoint
{
    public class LoadPointManager : ILoadPointManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly GravitasDbContext _context;
        private readonly ITicketRepository _ticketRepository;

        public LoadPointManager(IRoutesInfrastructure routesInfrastructure, 
            GravitasDbContext context, 
            ITicketRepository ticketRepository)
        {
            _routesInfrastructure = routesInfrastructure;
            _context = context;
            _ticketRepository = ticketRepository;
        }

        public bool ConfirmLoadGuide(int ticketId, Guid employeeId)
        {
            var loadGuideOpData = _context.LoadGuideOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (loadGuideOpData == null)
            {
                _logger.Error($"LoadPointManager: BindLoadPoint: Disable LoadOpData with ticket ={ticketId}");
                return false;
            }

            var visa = new Model.DomainModel.OpVisa.DAO.OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження призначення точки завантаження",
                LoadGuideOpDataId = loadGuideOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointGuide.State.AddOpVisa
            };
            _context.OpVisas.Add(visa);
            _context.SaveChanges();

            loadGuideOpData.StateId = OpDataState.Processed;
            loadGuideOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<LoadGuideOpData, Guid>(loadGuideOpData);
            _logger.Info($"LoadPointManager: BindLoadPoint: Loadpoint {loadGuideOpData.LoadPointNodeId} was assigned to ticket = {ticketId}");
            return true;
        }

        public bool ConfirmLoadPoint(int ticketId, Guid employeeId)
        {
            var loadPointOpData = _context.LoadPointOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (loadPointOpData == null) return false;
            if (loadPointOpData.StateId == OpDataState.Canceled || loadPointOpData.StateId == OpDataState.Rejected) return true;

            var visa = new Model.DomainModel.OpVisa.DAO.OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження завантаження",
                LoadPointOpDataId = loadPointOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LoadPointType1.State.AddOperationVisa
            };
            _context.OpVisas.Add(visa);
            _context.SaveChanges();

            loadPointOpData.StateId = OpDataState.Processed;
            loadPointOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<LoadPointOpData, Guid>(loadPointOpData);

            _routesInfrastructure.MoveForward(ticketId, loadPointOpData.NodeId.Value);
            return true;
        }
    }
}