using System.Collections.Generic;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
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
        void DeleteRole(long roleId);
        
        List<PhoneDictionary> GetEmployeePhones();
        void UpdateRolePhone(PhoneDictionary employee);
    }
}