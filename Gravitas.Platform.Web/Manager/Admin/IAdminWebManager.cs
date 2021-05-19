using System.Collections.Generic;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.Admin.QueuePriority;

namespace Gravitas.Platform.Web.Manager.Admin
{
    public interface IAdminWebManager
    {
        QueuePatternVm GetQueueTable();
        void UpdateQueuePatternItem(QueuePatternItemVm vm);
        void DeleteQueuePatternItem(QueuePatternItemVm vm);

        NodeEditListVm GetNodesEditTable();
        void UpdateNodeItem(NodeEditVm vm);

        NodesTrafficListVm GetWholeNodeTrafficTable();
        NodesTrafficListVm GetCurrentNodeTrafficTable();

        RoleTableVm GetRoleTableVm();
        void UpdateRole(RoleVm role);
        void CreateRole(RoleVm role);
        void DeleteRole(int roleId);
        
        List<PhoneDictionary> GetEmployeePhones();
        void UpdateRolePhone(PhoneDictionary employee);
    }
}