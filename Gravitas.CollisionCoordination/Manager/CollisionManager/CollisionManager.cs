using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.CollisionCoordination.Manager.LaboratoryData;
using Gravitas.CollisionCoordination.Messages;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Phones;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.CollisionCoordination.Manager.CollisionManager
{
    public class CollisionManager : ICollisionManager
    {
        private readonly IConnectManager _connectManager;
        private readonly ILaboratoryDataManager _laboratoryDataManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IOpDataRepository _opDataRepository;
        private readonly IPhonesRepository _phonesRepository;
        private readonly GravitasDbContext _context;

        public CollisionManager(IConnectManager connectManager,
            ILaboratoryDataManager laboratoryDataManager,
            IOpDataRepository opDataRepository, 
            IPhonesRepository phonesRepository, 
            GravitasDbContext context)
        {
            _connectManager = connectManager;
            _laboratoryDataManager = laboratoryDataManager;
            _opDataRepository = opDataRepository;
            _phonesRepository = phonesRepository;
            _context = context;
        }

        public IMessage CreateEmail(int ticketId, List<string> contactData) =>
            new Email(_connectManager, _laboratoryDataManager, ticketId, contactData);

        public IMessage CreateSms(int ticketId) => new Sms(_connectManager, ticketId);

        public void Approve(Guid opDataId, string approvedBy)
        {
            AddOpVisaRecord(opDataId, $@"Погоджено менеджером({approvedBy})");
            UpdateOpDataRecord(opDataId, OpDataState.CollisionApproved);

            _logger.Info($"Collision coordination: collision approved. OpData={opDataId}");
        }

        public void Disapprove(Guid opDataId, string approvedBy)
        {
            AddOpVisaRecord(opDataId, $@"Відмовлено менеджером({approvedBy})");
            UpdateOpDataRecord(opDataId, OpDataState.CollisionDisapproved);

            _logger.Info($"Collision coordination: collision disapproved. OpData={opDataId}");
        }

        public bool IsOpDataValid(Guid opDataId) =>
            _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == opDataId)?.StateId == OpDataState.Collision ||
            _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == opDataId)?.StateId == OpDataState.Collision;
        
        public void SendCentralLabNotification(Guid opDataId, bool approved)
        {
            var opData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == opDataId);
            if (opData?.TicketId == null) return;

            var parameters = new Dictionary<string, object>
            {
                {"CollisionResult", approved ? "погодили" : "не погодили"}
            };

            try
            {
                _connectManager.SendSms(SmsTemplate.CentralLaboratoryCollisionProcessed, opData.TicketId, _phonesRepository.GetPhone(Phone.CentralLaboratoryWorker), parameters);
            }
            catch (Exception e)
            {
                _logger.Error($"SendCentralLabNotification: Error while sending sms: {e}");
            }
        }

        private void UpdateOpDataRecord(Guid opDataId, OpDataState state)
        {
            if (_context.LabFacelessOpDatas.Any(data => data.Id == opDataId))
            {
                var opData = _context.LabFacelessOpDatas.First(x => x.Id == opDataId);
                opData.StateId = state;
                _opDataRepository.Update<LabFacelessOpData, Guid>(opData);
            } else if (_context.CentralLabOpDatas.Any(data => data.Id == opDataId))
            {
                var opData = _context.CentralLabOpDatas.First(x => x.Id == opDataId);
                opData.StateId = state;
                _opDataRepository.Update<CentralLabOpData, Guid>(opData);
            }
            
        }

        private void AddOpVisaRecord(Guid opDataId, string message)
        {
            if (_context.LabFacelessOpDatas.Any(data => data.Id == opDataId))
            {
                _opDataRepository.Add<OpVisa, int>(new OpVisa
                {
                    DateTime = DateTime.Now,
                    Message = message,
                    LabFacelessOpDataId = opDataId,
                    OpRoutineStateId = OpRoutine.LaboratoryIn.State.PrintCollisionManage
                });
            }
            else if (_context.CentralLabOpDatas.Any(data => data.Id == opDataId))
            {
                _opDataRepository.Add<OpVisa, int>(new OpVisa
                {
                    DateTime = DateTime.Now,
                    Message = message,
                    CentralLaboratoryOpData = opDataId,
                    OpRoutineStateId = OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit
                });
            }

            
        }
    }
}