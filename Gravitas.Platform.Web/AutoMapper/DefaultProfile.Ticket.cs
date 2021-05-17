using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.AutoMapper {

	public partial class DefaultProfile {

		public void ConfigureTicket() {

			CreateMap<Model.Dto.TicketItem, TicketItemVm>().ReverseMap();
			CreateMap< Model.Dto.TicketItems, TicketItemsVm >().ReverseMap();
		}
	}
}