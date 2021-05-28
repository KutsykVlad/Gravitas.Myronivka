using System.ComponentModel;

namespace Gravitas.Model.DomainValue
{
    public enum TicketStatus
    {
        [Description("Новий")]
        New = 1,
        
        [Description("Бланк")]
        Blank = 2,
        
        [Description("До опрацювання")]
        ToBeProcessed = 3,
        
        [Description("В роботі")]
        Processing = 4,
        
        [Description("Завершено")]
        Completed = 5,
        
        [Description("Проведено")]
        Closed = 6,
        
        [Description("Скасовано")]
        Canceled = 10
    }
}