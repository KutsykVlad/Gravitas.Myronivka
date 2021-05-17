using System;
using Gravitas.Platform.Web.Models;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.OpData.NonStandart;

namespace Gravitas.Platform.Web.Manager.OpData
{
    public interface IOpDataWebManager
    {
        LaboratoryInVms.LabFacelessOpDataComponentItemsVm LaboratoryIn_GetListComponentItems(Guid opDataId);
        NonStandartRegistryItemsVm GetNonStandardRegistryItems(NonStandartDataFilters nonstandartFilters, int pageNumber = 1, int pageSize = 25);
        OpCameraImageItemsVm GetOpCameraImageItems(Guid opDataId);
        OpDataItemsVm GetOpDataItems(long ticketId);
    }
}