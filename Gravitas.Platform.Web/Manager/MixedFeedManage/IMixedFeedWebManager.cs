using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.MixedFeedManage
{
    public interface IMixedFeedWebManager
    {
        void Edit_Back();
        MixedFeedManageVms.EditVm GetEditVm();
        void Edit_Save(MixedFeedManageVms.EditVm mixedFeedSilo);
        MixedFeedManageVms.WorkstationSiloVm GetSiloItems();
        void Workstation_SelectSilo(int siloId);
        void Edit_Clear();
    }
}