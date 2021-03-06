using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.CentralLaboratory
{
    public class CentralLaboratoryManager : ICentralLaboratoryManager
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly GravitasDbContext _context;

        public CentralLaboratoryManager(IOpDataRepository opDataRepository, GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _context = context;
        }

        public CentralLabState GetTicketStateInCentralLab(int? ticketId)
        {
            var result = CentralLabState.InQueue;
            if (ticketId == null) return result;
            var centralLabOpData = _context.CentralLabOpDatas
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.SampleCheckInDateTime)
                .FirstOrDefault();

            if (centralLabOpData == null)
            {
                var lastOpData = _opDataRepository.GetLastOpData(ticketId);
                if (lastOpData?.NodeId == null) return result;
                switch (lastOpData.NodeId.Value)
                {
                    case (int) NodeIdValue.SecurityIn1:
                        result = CentralLabState.SecurityEntry;
                        break;
                    case (int) NodeIdValue.Weighbridge1:
                    case (int) NodeIdValue.Weighbridge2:
                    case (int) NodeIdValue.Weighbridge3:
                    case (int) NodeIdValue.Weighbridge4:
                        result = CentralLabState.Weighbridge;
                        break;
                }

                if (_opDataRepository.GetLastOpData<LoadPointOpData>(ticketId, OpDataState.Init) != null 
                    || _opDataRepository.GetLastOpData<LoadPointOpData>(ticketId, OpDataState.Processed) != null
                    || _opDataRepository.GetLastOpData<LoadPointOpData>(ticketId, OpDataState.Processing) != null) result = CentralLabState.Loaded;
            }
            else
            {
                if (centralLabOpData.SampleCheckInDateTime.HasValue && !centralLabOpData.SampleCheckOutTime.HasValue)
                    result = CentralLabState.WaitForSamplesCollect;
                if (centralLabOpData.SampleCheckInDateTime.HasValue && centralLabOpData.SampleCheckOutTime.HasValue)
                    result = CentralLabState.SamplesCollected;
                if (!string.IsNullOrEmpty(centralLabOpData.LaboratoryComment)) result = CentralLabState.WaitForOperator;
                if (centralLabOpData.StateId == OpDataState.Collision) result = CentralLabState.OnCollision;
                if (centralLabOpData.StateId == OpDataState.CollisionApproved) result = CentralLabState.CollisionApproved;
                if (centralLabOpData.StateId == OpDataState.CollisionDisapproved) result = CentralLabState.CollisionDisapproved;
                if (centralLabOpData.StateId == OpDataState.Processed) result = CentralLabState.Processed;
                if (centralLabOpData.StateId == OpDataState.Canceled) result = CentralLabState.ReturnToCollectSamples;
                if (centralLabOpData.StateId == OpDataState.Rejected) result = CentralLabState.Moved;
            }

            return result;
        }

        public Dictionary<CentralLabState, string> LabStateName { get; } = new Dictionary<CentralLabState, string>
        {
            {
                CentralLabState.InQueue, "В черзі"
            },
            {
                CentralLabState.SecurityEntry, "КПП в'їзд"
            },
            {
                CentralLabState.Weighbridge, "Вагова"
            },
            {
                CentralLabState.Loaded, "Завантаження"
            },
            {
                CentralLabState.WaitForSamplesCollect, "Очікування відбору проб"
            },
            {
                CentralLabState.SamplesCollected, "Проби відібрані"
            },
            {
                CentralLabState.WaitForOperator, "Очікування рішення майстра"
            },
            {
                CentralLabState.OnCollision, "На погодженні"
            },
            {
                CentralLabState.CollisionApproved, "Погоджено"
            },
            {
                CentralLabState.CollisionDisapproved, "Відмовлено у погодженні"
            },
            {
                CentralLabState.ReturnToCollectSamples, "Повторний відбір проб"
            },
            {
                CentralLabState.Moved, "На переміщенні"
            },
            {
                CentralLabState.Processed, "Опрацьовано"
            }
        };

        public Dictionary<CentralLabState, int> LabStateOrder { get; } = new Dictionary<CentralLabState, int>
        {
            {
                CentralLabState.InQueue, 6
            },
            {
                CentralLabState.SecurityEntry, 6
            },
            {
                CentralLabState.Weighbridge, 6
            },
            {
                CentralLabState.Loaded, 2
            },
            {
                CentralLabState.WaitForSamplesCollect, 3
            },
            {
                CentralLabState.SamplesCollected, 4
            },
            {
                CentralLabState.WaitForOperator, 5
            },
            {
                CentralLabState.OnCollision, 5
            },
            {
                CentralLabState.CollisionApproved, 1
            },
            {
                CentralLabState.CollisionDisapproved, 7
            },
            {
                CentralLabState.ReturnToCollectSamples, 8
            },
            {
                CentralLabState.Moved, 99
            },
            {
                CentralLabState.Processed, 100
            }
        };

        public Dictionary<CentralLabState, string> LabStateClassStyle { get; } = new Dictionary<CentralLabState, string>
        {
            {
                CentralLabState.InQueue, "table-default "
            },
            {
                CentralLabState.SecurityEntry, "table-default "
            },
            {
                CentralLabState.Weighbridge, "table-default "
            },
            {
                CentralLabState.Loaded, "table-secondary "
            },
            {
                CentralLabState.WaitForSamplesCollect, "table-primary "
            },
            {
                CentralLabState.SamplesCollected, "table-warning "
            },
            {
                CentralLabState.WaitForOperator, "table-danger "
            },
            {
                CentralLabState.OnCollision, "table-danger "
            },
            {
                CentralLabState.CollisionApproved, "table-success "
            },
            {
                CentralLabState.CollisionDisapproved, "table-dark "
            },
            {
                CentralLabState.Processed, "table-light "
            },
            {
                CentralLabState.ReturnToCollectSamples, "table-light "
            },
            {
                CentralLabState.Moved, "table-light "
            }
        };
    }
}