using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class PreRegisterTicketContainerItemVm
    {
        [DisplayName("Номер телефону")]
        public string PhoneNo { get; set; }
        [DisplayName("Орієнтовний час реєстрації")]
        public DateTime PredictionEntranceTime { get; set; }
        [DisplayName("Номер автомобіля")]
        public string TruckNumber { get; set; }
        [DisplayName("Нотатки")]
        public string Notice { get; set; }
    }
}