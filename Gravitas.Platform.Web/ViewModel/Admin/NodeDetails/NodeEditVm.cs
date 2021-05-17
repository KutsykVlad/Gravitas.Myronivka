using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class NodeEditVm
    {
        [DisplayName("Ідентифікатор")]
        public long Id { get; set; }
        [DisplayName("Назва вузла")]
        public string Name { get; set; }
        [DisplayName("Максимально дозволений час обробки, хв.")]
        public long MaximumProcessingTime { get; set; }
        [DisplayName("Квота автомобілів, шт.")]
        public int Quota { get; set; }
    }
}