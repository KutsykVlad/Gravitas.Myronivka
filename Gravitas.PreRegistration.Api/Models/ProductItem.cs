using System;
using System.Collections.Generic;

namespace Gravitas.PreRegistration.Api.Models
{
    public class ProductItem
    {
        public long RouteId { get; set; }
        public string Title { get; set; }
        public List<DateTime> BusyDateTimeList { get; set; }
        public int RouteTimeInMinutes { get; set; }
        public int TrucksInQueue { get; set; }
    }
}