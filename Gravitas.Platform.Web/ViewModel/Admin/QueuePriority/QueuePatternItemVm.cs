using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public class QueuePatternItemVm
    {
        public long QueuePatternItemId { get; set; }
        [DisplayName("Ім`я власника")]
        public string ReceiverName { get; set; }

        public string ReceiverId { get; set; }
        [DisplayName("Пріоритет")]
        public long Priority { get; set; }
        public string PriorityDescription { get; set; }

        [DisplayName("Кількість машин")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Count { get; set; }

        [DisplayName("Тип")]
        public long Category { get; set; }
        public string CategoryDescription { get; set; }

        public bool IsFixed { get; set; }
    }
}