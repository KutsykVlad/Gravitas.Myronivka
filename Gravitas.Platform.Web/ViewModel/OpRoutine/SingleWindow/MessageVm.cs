using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class MessageVm
        {
            public int Id { get; set; }
            
            [DisplayName("Картка")]
            public int CardNo { get; set; }
            
            [DisplayName("Транспорт")]
            public string TruckNo { get; set; }
            
            [DisplayName("Повідомлення")]
            public string Message { get; set; }
            
            [DisplayName("Отримувач")]
            public string Receiver { get; set; }
            
            [DisplayName("Дата")]
            public DateTime Created { get; set; }
        }
    }
}