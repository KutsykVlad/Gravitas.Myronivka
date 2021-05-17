using System;
using System.Collections.Generic;

namespace Gravitas.PreRegistration.Api.Models
{
    public class ProductItemDto
    {
        public long RouteId { get; set; }
        public string Title { get; set; }
        public List<DateTime> FreeDateTimeList { get; set; }
        public int TrucksInQueue { get; set; }
    }
}