using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.Node.TDO.List;

namespace Gravitas.DAL.Repository.Node
{
    public interface INodeRepository : IBaseRepository
    {
        NodeItems GetNodeItems();
        NodeItem GetNodeItem(int id);
        Model.DomainModel.Node.TDO.Detail.NodeDetails GetNodeDto(int? nodeId);
        NodeContext GetNodeContext(int nodeId);
        bool UpdateNodeContext(int nodeId, NodeContext newContext);
        void UpdateNodeProcessingMessage(int nodeId, NodeProcessingMsgItem msgItem);
        void ClearNodeProcessingMessage(int nodeId);
    }
}