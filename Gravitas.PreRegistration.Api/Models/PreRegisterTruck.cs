using System;

namespace Gravitas.PreRegistration.Api.Models
{
    public class PreRegisterTruck
    {
        public long Id { get; set; }
        public string PhoneNo { get; set; }
        public string RouteTitle { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string TruckNumber { get; set; }
        public string Notice { get; set; }
    }
}