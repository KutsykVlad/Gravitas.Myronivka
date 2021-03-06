using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel.Queue {
    public class ExternalQueueVm
    {
        [Required]
        public IQueryable<ExternalQueueItemVm> Items { get; set; }
    }
}