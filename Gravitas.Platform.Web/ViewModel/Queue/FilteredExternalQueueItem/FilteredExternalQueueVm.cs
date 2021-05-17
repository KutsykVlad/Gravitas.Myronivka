using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Gravitas.Platform.Web.ViewModel.Queue
{
    public class FilteredExternalQueueVm
    {
        [Required]
        public IList<FilteredExternalQueueItemVm> Items { get; set; }
        public string ProductId { get; set; }
    }
}