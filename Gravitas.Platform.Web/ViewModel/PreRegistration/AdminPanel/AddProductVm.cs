using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel.PreRegistration.AdminPanel
{
    public class AddProductVm
    {
        [Required]
        [DisplayName("Маршрут")]
        public int RouteTemplateId { get; set; }

        [Required]
        [DisplayName("Назва")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Тривалість не менше 60(хв)")]
        public int RouteTimeInMinutes { get; set; }

        public List<SelectListItem> Routes { get; set; }
    }
}