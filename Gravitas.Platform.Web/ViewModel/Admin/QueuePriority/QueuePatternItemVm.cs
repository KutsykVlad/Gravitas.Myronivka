using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel.Admin.QueuePriority
{
    public class QueuePatternItemVm
    {
        public int QueuePatternItemId { get; set; }
        [DisplayName("Ім`я власника")]
        public string ReceiverName { get; set; }

        public Guid? ReceiverId { get; set; }
        [DisplayName("Пріоритет")]
        public Model.DomainValue.QueuePriority Priority { get; set; }
        public string PriorityDescription { get; set; }

        [DisplayName("Кількість машин")]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Count { get; set; }

        [DisplayName("Тип")]
        public QueueCategory Category { get; set; }
        public string CategoryDescription { get; set; }

        public bool IsFixed { get; set; }
    }
}