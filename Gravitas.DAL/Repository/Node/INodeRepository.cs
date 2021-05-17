using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.Node.TDO.List;

namespace Gravitas.DAL
{

	public interface INodeRepository : IBaseRepository<GravitasDbContext> {

		NodeItems GetNodeItems();
		NodeItem GetNodeItem(long id);
		
		Node GetNodeDto(long? nodeId);
		NodeContext GetNodeContext(long nodeId);

	    
		bool UpdateNodeContext(long nodeId, NodeContext newContext, long processorId);

		void UpdateNodeProcessingMessage(long nodeId, NodeProcessingMsgItem msgItem);
		void ClearNodeProcessingMessage(long nodeId);
	    bool IsFirstState(long nodeId, NodeContext newContext, long processorId);

	}
}
