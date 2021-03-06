using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel.Queue {
    public class ExternalQueueItemVm
    {
        [Required]
        [DisplayName("ID контейнеру")]
        public int? TicketContainerId { get; set; }

        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DisplayName("Номер причепу")]
        public string TrailerPlate { get; set; }

        [DisplayName("Номер машини")]
        public string TruckPlate { get; set; }

        [DisplayName("Id маршруту")]
        public int? RouteId { get; set; }

        [DisplayName("В'їзд дозволено")]
        public bool IsAllowedToEnterTerritory { get; set; }

    }
}