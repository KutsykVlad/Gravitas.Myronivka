using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;

namespace Gravitas.Infrastructure.Platform.Manager.Node
{
    public class NodeManager : INodeManager
    {
        private readonly INodeRepository _nodeRepository;
        private readonly GravitasDbContext _context;

        public NodeManager(INodeRepository nodeRepository, GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _context = context;
        }

        public bool IsNodeExpired(int nodeId, TimeSpan timeout)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            return nodeDto?.Context?.LastStateChangeTime == null
                   || DateTime.Now - nodeDto.Context.LastStateChangeTime.Value > timeout;
        }

        public void ChangeNodeState(int nodeId, bool state)
        {
            var node = _context.Nodes.FirstOrDefault(x => x.Id == nodeId);
            if (node == null) return;

            node.IsActive = state;
            _context.SaveChanges();
        }

        public string GetNodeName(int nodeId)
        {
            var nodeName = _context.Nodes.FirstOrDefault(x => x.Id == nodeId)?.Name;
            return nodeName ?? string.Empty;
        }

        public List<int> GetEndPointNodes()
        {
            return new List<int>();
        }
    }
}