namespace Gravitas.Platform.Web.ViewModel {

	public static partial class SingleWindowVms {

		public class EditTicketFormVm {
			public long NodeId { get; set; }
			public long OpDataId { get; set; }

			public SingleWindowOpDataDetailVm SingleWindowOpDataDetailVm { get; set; }

			public bool IsEditable { get; set; }
		}
    }
}