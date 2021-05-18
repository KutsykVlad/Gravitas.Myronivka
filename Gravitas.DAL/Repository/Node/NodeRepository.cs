using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.DAL
{
    public class NodeRepository : BaseRepository<GravitasDbContext>, INodeRepository
    {
        private readonly GravitasDbContext _context;

        public NodeRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public NodeItems GetNodeItems()
        {
            var dao = GetQuery<Model.DomainModel.Node.DAO.Node, long>().ToList();
            var dto = new NodeItems
            {
                Items = dao.Select(GetNodeItem).ToList()
            };
            return dto;
        }

        public NodeItem GetNodeItem(long id)
        {
            var dao = _context.Nodes.AsNoTracking().First(x => x.Id == id);
            var dto = GetNodeItem(dao);
            return dto;
        }

        public Node GetNodeDto(long? nodeId)
        {
            if (nodeId == null) return null;
            var node = _context.Nodes.AsNoTracking().First(x => x.Id == nodeId.Value);
            return node is null
                ? null
                : new Node
                {
                    Id = node.Id,
                    OrganisationUnitId = node.OrganisationUnitId,
                    OpRoutineId = node.OpRoutineId,
                    Code = node.Code,
                    Name = node.Name,
                    IsActive = node.IsActive,
                    IsStart = node.IsStart,
                    IsFinish = node.IsFinish,
                    IsEmergency = node.IsEmergency,
                    Quota = node.Quota,
                    MaximumProcessingTime = node.MaximumProcessingTime,
                    Config = NodeConfig.FromJson(node.Config),
                    Context = NodeContext.FromJson(node.Context),
                    ProcessingMessage = NodeProcessingMsg.FromJson(node.ProcessingMessage), 
                    Group = node.NodeGroup
                };
        }

        public NodeContext GetNodeContext(long nodeId)
        {
            var node = _context.Nodes.AsNoTracking().First(x => x.Id == nodeId);
            return NodeContext.FromJson(node.Context);
        }

        public bool IsFirstState(long nodeId, NodeContext newContext, long processorId)
        {
            var nodeDto = GetNodeDto(nodeId);

            var firstStateId = _context.OpRoutineTransitions.Where(s => s.OpRoutineId == nodeDto.OpRoutineId
                                                                        && s.ProcessorId == processorId
            ).Min(s => s.StartStateId);

            return nodeDto.Context.OpRoutineStateId == firstStateId;
        }

        public bool UpdateNodeContext(long nodeId, NodeContext newContext, long processorId)
        {
            var nodeDto = GetNodeDto(nodeId);

            var isTransitionValid = _context.OpRoutineTransitions.Any(e =>
                e.OpRoutineId == nodeDto.OpRoutineId
                && e.StartStateId == nodeDto.Context.OpRoutineStateId
                && e.StopStateId == newContext.OpRoutineStateId
                && e.ProcessorId == processorId);

            if (nodeDto.Context.OpRoutineStateId != newContext?.OpRoutineStateId
                && !isTransitionValid)
                return false;

            if (newContext != null)
                newContext.LastStateChangeTime = DateTime.Now;

            return UpdateNodeContextJson(nodeId, newContext?.ToJson());
        }

        public void UpdateNodeProcessingMessage(long nodeId, NodeProcessingMsgItem msgItem)
        {
            var node = GetEntity<Model.DomainModel.Node.DAO.Node, long>(nodeId);
            if (node == null) return;
            node.ProcessingMessage = msgItem == null
                ? string.Empty
                : new NodeProcessingMsg
                {
                    Items = new List<NodeProcessingMsgItem>
                    {
                        msgItem
                    }
                }.ToJson();

            Update<Model.DomainModel.Node.DAO.Node, long>(node);
        }

        public void ClearNodeProcessingMessage(long nodeId)
        {
            var node = GetEntity<Model.DomainModel.Node.DAO.Node, long>(nodeId);
            if (node == null) return;
            node.ProcessingMessage = new NodeProcessingMsg().ToJson();
            Update<Model.DomainModel.Node.DAO.Node, long>(node);
        }

        private NodeItem GetNodeItem(Model.DomainModel.Node.DAO.Node dao)
        {
            if (dao == null) return null;

            return new NodeItem
            {
                Id = dao.Id,
                Code = dao.Code,
                Name = dao.Name,
                OpRoutineId = dao.OpRoutineId,
                OpRoutineName = dao.OpRoutine.Name
            };
        }

        private bool UpdateNodeContextJson(long nodeId, string newContextJson)
        {
            var node = _context.Nodes.FirstOrDefault(x => x.Id == nodeId);
            if (node == null) return false;

            node.Context = newContextJson;
            _context.SaveChanges();
            return true;
        }
    }
}