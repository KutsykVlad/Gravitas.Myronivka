using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Infrastructure.Platform.Manager.UnloadPoint
{
    public class UnloadPointManager : IUnloadPointManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly GravitasDbContext _context;
        private readonly ITicketRepository _ticketRepository;

        public UnloadPointManager(GravitasDbContext context, ITicketRepository ticketRepository)
        {
            _context = context;
            _ticketRepository = ticketRepository;
        }

        public bool ConfirmUnloadGuide(long ticketId, string employeeId)
        {
            var unloadGuideOpData = _context.UnloadGuideOpDatas.AsNoTracking()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (unloadGuideOpData == null)
            {
                _logger.Error($"LoadPointManager: BindUnloadPoint: Disable UnloadGuideOpData with ticket ={ticketId}");
                return false;
            }

            var unloadVisa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження призначення ями вигрузки",
                UnloadGuideOpDataId = unloadGuideOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.AddOpVisa
            };
            _context.OpVisas.Add(unloadVisa);
            _context.SaveChanges();

            unloadGuideOpData.StateId = Dom.OpDataState.Processed;
            unloadGuideOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<UnloadGuideOpData, Guid>(unloadGuideOpData);
            _logger.Info($"LoadPointManager: BindLoadPoint: Unloadpoint {unloadGuideOpData.UnloadPointNodeId} was assigned to ticket = {ticketId}");
            return true;
        }

        public bool ConfirmUnloadPoint(long ticketId, string employeeId)
        {
            var unloadPointOpData = _context.UnloadPointOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (unloadPointOpData == null) return false;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження вигрузки",
                UnloadPointOpDataId = unloadPointOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.AddOperationVisa
            };
            _context.OpVisas.Add(visa);
            _context.SaveChanges();

            unloadPointOpData.StateId = Dom.OpDataState.Processed;
            unloadPointOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<UnloadPointOpData, Guid>(unloadPointOpData);

            return true;
        }
    }
}