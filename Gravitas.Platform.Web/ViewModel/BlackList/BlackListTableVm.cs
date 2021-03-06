using Gravitas.Platform.Web.ViewModel.BlackList.List;

namespace Gravitas.Platform.Web.ViewModel.BlackList
{
    public class BlackListTableVm
    {
        public BlackListDriversListVm Drivers { get; set; }
        public BlackListPartnersListVm Partners { get; set; }
        public BlackListTrailersListVm Trailers { get; set; }
        public BlackListTransportListVm Transport { get; set; }

        public int ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}