using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.Admin.NodeDetails
{
    public class NodeEditVm
    {
        [DisplayName("Ідентифікатор")]
        public int Id { get; set; }
        [DisplayName("Назва вузла")]
        public string Name { get; set; }
        [DisplayName("Максимально дозволений час обробки, хв.")]
        public int MaximumProcessingTime { get; set; }
        [DisplayName("Квота автомобілів, шт.")]
        public int Quota { get; set; }
    }
}