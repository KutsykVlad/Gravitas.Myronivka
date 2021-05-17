using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.RouteEditor
{
    public class RoutesListItemVm
    {
        public long Id { get; set; }

        [DisplayName("Назва маршруту")]
        public string Name { get; set; }
    }
}