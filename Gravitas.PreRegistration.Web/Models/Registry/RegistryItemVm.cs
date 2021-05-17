using System;
using System.ComponentModel;

namespace Gravitas.PreRegistration.Web.Models.Registry
{
    public class RegistryItemVm
    {
        public string Id { get; set; }
        [DisplayName("Номер авто")]
        public string TruckNo { get; set; }
        [DisplayName("Номер причепу")]
        public string TrailerNo { get; set; }
        [DisplayName("Телефон водія")]
        public string PhoneNo { get; set; }
        public long RouteId { get; set; }
        [DisplayName("Час прибуття")]
        public DateTime EntranceTime { get; set; }
    }
}