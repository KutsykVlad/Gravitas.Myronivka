using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.AutoMapper {

	public partial class DefaultProfile {

		public void ConfigureNode() {

			CreateMap<Node, NodeDetailVm>().ReverseMap();

			CreateMap<NodeContext, NodeContextVm>().ReverseMap();
			CreateMap<NodeProcessingMsg, NodeProcessingMsgVm>().ReverseMap();
			CreateMap<NodeProcessingMsgItem, NodeProcessingMsgItemVm>().ReverseMap();
		}
	}
}