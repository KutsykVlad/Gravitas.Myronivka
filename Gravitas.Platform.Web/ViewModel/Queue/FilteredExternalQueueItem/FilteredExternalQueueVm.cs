using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel.Queue.FilteredExternalQueueItem
{
    public class FilteredExternalQueueVm
    {
        [Required]
        public IList<FilteredExternalQueueItemVm> Items { get; set; }
        public Guid ProductId { get; set; }
    }
}