using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
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

        public bool ConfirmLoadGuide(long ticketId, string employeeId)
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

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження призначення точки завантаження",
                LoadGuideOpDataId = loadGuideOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.AddOpVisa
            };
            _context.OpVisas.Add(visa);
            _context.SaveChanges();

            loadGuideOpData.StateId = Dom.OpDataState.Processed;
            loadGuideOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<LoadGuideOpData, Guid>(loadGuideOpData);
            _logger.Info($"LoadPointManager: BindLoadPoint: Loadpoint {loadGuideOpData.LoadPointNodeId} was assigned to ticket = {ticketId}");
            return true;
        }

        public bool ConfirmLoadPoint(long ticketId, string employeeId)
        {
            var loadPointOpData = _context.LoadPointOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (loadPointOpData == null) return false;
            if (loadPointOpData.StateId == Dom.OpDataState.Canceled || loadPointOpData.StateId == Dom.OpDataState.Rejected) return true;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження завантаження",
                LoadPointOpDataId = loadPointOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa
            };
            _context.OpVisas.Add(visa);
            _context.SaveChanges();

            loadPointOpData.StateId = Dom.OpDataState.Processed;
            loadPointOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<LoadPointOpData, Guid>(loadPointOpData);

            _routesInfrastructure.MoveForward(ticketId, loadPointOpData.NodeId.Value);
            return true;
        }
    }
}