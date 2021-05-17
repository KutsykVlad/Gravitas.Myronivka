using System;

namespace Gravitas.PreRegistration.Api.Models
{
    public class AddToQueueItem
    {
        public long RouteId { get; set; }
        public string PhoneNo { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string TruckNumber { get; set; }
        public string Notice { get; set; }
    }
}