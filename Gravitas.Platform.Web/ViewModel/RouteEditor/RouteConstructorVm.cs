using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel.RouteEditor {
    public class RouteConstructorVm {
        
        [Required]
        [DisplayName("Назва")]
        public string Name { get; set; }
        
        [Required]
        public string RouteJson { get; set; }

        public IEnumerable<SelectListItem> NodeItems { get; set; }
    }
}