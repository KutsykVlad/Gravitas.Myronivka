using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.AutoMapper {

	public partial class DefaultProfile {

		public void ConfigureNode() {

			CreateMap<Model.Dto.Node, NodeDetailVm>().ReverseMap();

			CreateMap<Model.Dto.NodeContext, NodeContextVm>().ReverseMap();
			CreateMap<Model.Dto.NodeProcessingMsg, NodeProcessingMsgVm>().ReverseMap();
			CreateMap<Model.Dto.NodeProcessingMsgItem, NodeProcessingMsgItemVm>().ReverseMap();
		}
	}
}