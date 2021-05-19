using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.BlackList
{
    public interface IBlackListManager
    {
        BlackListTableVm GetBlackListTable();

        (bool,string) AddPartnerRecord(BlackListPartnerRecordToAddVm partner);
        (bool, string) AddDriverRecord(BlackListDriverRecordVm driver);
        (bool, string) AddTrailerRecord(BlackListTrailerRecordVm trailer);
        (bool, string) AddTransportRecord(BlackListTransportRecordVm trailer);
        
        void DeleteBlackListDriverRecord(int driverId);
        void DeleteBlackListTrailerRecord(int trailerId);
        void DeleteBlackListTransportRecord(int transportId);
        void DeleteBlackListPartnerRecord(string partnerId);
    }
}
