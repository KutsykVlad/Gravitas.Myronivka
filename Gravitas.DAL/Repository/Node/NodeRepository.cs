using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Newtonsoft.Json;

namespace Gravitas.DAL.Repository.Node
{
    public class NodeRepository : BaseRepository, INodeRepository
    {
        private readonly GravitasDbContext _context;

        public NodeRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public NodeItems GetNodeItems()
        {
            var dao = GetQuery<Model.DomainModel.Node.DAO.Node, int>().ToList();
            var dto = new NodeItems
            {
                Items = dao.Select(GetNodeItem).ToList()
            };
            return dto;
        }

        public NodeItem GetNodeItem(int id)
        {
            var dao = _context.Nodes.AsNoTracking().First(x => x.Id == id);
            var dto = GetNodeItem(dao);
            return dto;
        }

        public Model.DomainModel.Node.TDO.Detail.Node GetNodeDto(int? nodeId)
        {
            if (nodeId == null) return null;
            var node = _context.Nodes.AsNoTracking().First(x => x.Id == nodeId.Value);
            return node is null
                ? null
                : new Model.DomainModel.Node.TDO.Detail.Node
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
                    Config = JsonConvert.DeserializeObject<NodeConfig>(node.Config),
                    Context = JsonConvert.DeserializeObject<NodeContext>(node.Context),
                    ProcessingMessage = JsonConvert.DeserializeObject<NodeProcessingMsg>(node.ProcessingMessage), 
                    Group = node.NodeGroup
                };
        }

        public NodeContext GetNodeContext(int nodeId)
        {
            var node = _context.Nodes.AsNoTracking().First(x => x.Id == nodeId);
            return JsonConvert.DeserializeObject<NodeContext>(node.Context);
        }

        public bool UpdateNodeContext(int nodeId, NodeContext newContext)
        {
            var nodeDto = GetNodeDto(nodeId);


            if (nodeDto.Context.OpRoutineStateId != newContext?.OpRoutineStateId)
                return false;

            if (newContext != null)
                newContext.LastStateChangeTime = DateTime.Now;

            return UpdateNodeContextJson(nodeId, JsonConvert.SerializeObject(newContext));
        }

        public void UpdateNodeProcessingMessage(int nodeId, NodeProcessingMsgItem msgItem)
        {
            var node = _context.Nodes.FirstOrDefault(x => x.Id == nodeId);
            if (node == null) return;
            node.ProcessingMessage = msgItem == null
                ? string.Empty
                : JsonConvert.SerializeObject(new NodeProcessingMsg
                {
                    Items = new List<NodeProcessingMsgItem>
                    {
                        msgItem
                    }
                });

            Update<Model.DomainModel.Node.DAO.Node, int>(node);
        }

        public void ClearNodeProcessingMessage(int nodeId)
        {
            var node = _context.Nodes.FirstOrDefault(x => x.Id == nodeId);
            if (node == null) return;
            node.ProcessingMessage = JsonConvert.SerializeObject(new NodeProcessingMsg());
            Update<Model.DomainModel.Node.DAO.Node, int>(node);
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