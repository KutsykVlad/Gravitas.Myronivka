using System;
using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class TicketHistoryItems
    {
        public List<TicketHistoryItem> Items;
        public DateTime? Date { get; set; }
        public string Query { get; set; }
        public int PrevPage { get; set; }
        public int NextPage { get; set; }
        public int ItemsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}