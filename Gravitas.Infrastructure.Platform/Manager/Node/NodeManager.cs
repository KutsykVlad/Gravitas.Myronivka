using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EndPointNodes.DAO;
using Gravitas.Model.DomainModel.Node.DAO;

namespace Gravitas.Infrastructure.Platform.Manager
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

        public bool IsNodeExpired(long nodeId, TimeSpan timeout)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            return nodeDto?.Context?.LastStateChangeTime == null
                   || DateTime.Now - nodeDto.Context.LastStateChangeTime.Value > timeout;
        }

        public void ChangeNodeState(long nodeId, bool state)
        {
            var node = _nodeRepository.GetEntity<Node, long>(nodeId);
            if (node == null) return;

            node.IsActive = state;
            _nodeRepository.Update<Node, long>(node);
        }

        public string GetNodeName(long nodeId)
        {
            var nodeName = _context.Nodes.FirstOrDefault(x => x.Id == nodeId)?.Name;
            return nodeName ?? string.Empty;
        }

        private List<long> _endPoints;
        public List<long> GetEndPointNodes()
        {
            if (_endPoints == null)
            {
                _endPoints = _nodeRepository.GetQuery<EndPointNode, int>()
                    .Select(x => x.NodeId)
                    .ToList();
            }
            return _endPoints;
        }
    }
}