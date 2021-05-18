using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Traffic.DAO;
using Gravitas.Model.DomainModel.Traffic.TDO;

namespace Gravitas.DAL.Repository.Traffic
{
    public class TrafficRepository : BaseRepository, ITrafficRepository
    {
        private readonly GravitasDbContext _context;

        public TrafficRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public void OnNodeArrival(TrafficRecord record)
        {
            Add<TrafficHistory, int>(new TrafficHistory
            {
                NodeId = record.CurrentNodeId, EntranceTime = record.EntranceTime, TicketContainerId = record.TicketContainerId
            });
        }

        public void OnNodeHandle(int ticketContainerId, int nodeId)
        {
            var prev = _context.TrafficHistories.FirstOrDefault(r => r.TicketContainerId == ticketContainerId && r.NodeId == nodeId);
            if (prev == null) return;
            prev.DepartureTime = DateTime.Now;
            _context.SaveChanges();
        }

        public NodeTrafficHistory GetNodeTrafficHistory(int nodeId)
        {
            var result = new NodeTrafficHistory
            {
                NodeId = nodeId,
                Items = _context.TrafficHistories.Where(t => t.NodeId == nodeId).ToList()
            };

            result.Count = result.Items.Count;
            return result;
        }
    }
}