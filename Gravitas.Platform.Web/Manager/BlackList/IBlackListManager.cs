using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public interface IBlackListManager
    {
        BlackListTableVm GetBlackListTable();

        (bool,string) AddPartnerRecord(BlackListPartnerRecordToAddVm partner);
        (bool, string) AddDriverRecord(BlackListDriverRecordVm driver);
        (bool, string) AddTrailerRecord(BlackListTrailerRecordVm trailer);
        (bool, string) AddTransportRecord(BlackListTransportRecordVm trailer);
        
        void DeleteBlackListDriverRecord(long driverId);
        void DeleteBlackListTrailerRecord(long trailerId);
        void DeleteBlackListTransportRecord(long transportId);
        void DeleteBlackListPartnerRecord(string partnerId);
    }
}
