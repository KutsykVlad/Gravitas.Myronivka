using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel.RouteEditor {
    public class RouteQuataVm
    {
        [Required]
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string RouteJson { get; set; }
        public IEnumerable<SelectListItem> Quatas { get; set; }
        [Required]
        public IList<string> SelectedGroups { get; set; }
        
    }
}