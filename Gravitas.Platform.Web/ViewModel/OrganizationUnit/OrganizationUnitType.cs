using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.OrganizationUnit
{
    public class OrganizationUnitTypeVm
    {
        public string Name { get; set; }

        public virtual ICollection<Model.DomainModel.OrganizationUnit.DAO.OrganizationUnit> OrganizationUnitSet { get; set; }
    }
}