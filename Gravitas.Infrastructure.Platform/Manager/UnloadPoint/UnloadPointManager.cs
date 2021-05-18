using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

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

        public bool ConfirmUnloadGuide(int ticketId, string employeeId)
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

            var unloadVisa = new Model.DomainModel.OpVisa.DAO.OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження призначення ями вигрузки",
                UnloadGuideOpDataId = unloadGuideOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointGuide.State.AddOpVisa
            };
            _context.OpVisas.Add(unloadVisa);
            _context.SaveChanges();

            unloadGuideOpData.StateId = OpDataState.Processed;
            unloadGuideOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<UnloadGuideOpData, Guid>(unloadGuideOpData);
            _logger.Info($"LoadPointManager: BindLoadPoint: Unloadpoint {unloadGuideOpData.UnloadPointNodeId} was assigned to ticket = {ticketId}");
            return true;
        }

        public bool ConfirmUnloadPoint(int ticketId, string employeeId)
        {
            var unloadPointOpData = _context.UnloadPointOpDatas
                .AsNoTracking()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
            if (unloadPointOpData == null) return false;

            var visa = new Model.DomainModel.OpVisa.DAO.OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження вигрузки",
                UnloadPointOpDataId = unloadPointOpData.Id,
                EmployeeId = employeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.AddOperationVisa
            };
            _context.OpVisas.Add(visa);
            _context.SaveChanges();

            unloadPointOpData.StateId = OpDataState.Processed;
            unloadPointOpData.CheckOutDateTime = DateTime.Now;
            _ticketRepository.Update<UnloadPointOpData, Guid>(unloadPointOpData);

            return true;
        }
    }
}