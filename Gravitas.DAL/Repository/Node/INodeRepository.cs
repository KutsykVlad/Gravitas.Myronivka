namespace Gravitas.DAL
{

	public interface INodeRepository : IBaseRepository<GravitasDbContext> {

		Model.Dto.NodeItems GetNodeItems();
		Model.Dto.NodeItem GetNodeItem(long id);
		
		Model.Dto.Node GetNodeDto(long? nodeId);
		Model.Dto.NodeContext GetNodeContext(long nodeId);

	    
		bool UpdateNodeContext(long nodeId, Model.Dto.NodeContext newContext, long processorId);

		void UpdateNodeProcessingMessage(long nodeId, Model.Dto.NodeProcessingMsgItem msgItem);
		void ClearNodeProcessingMessage(long nodeId);
	    bool IsFirstState(long nodeId, Model.Dto.NodeContext newContext, long processorId);

	}
}
