using System.ComponentModel;

namespace Gravitas.Model.DomainValue
{
    public enum OpDataState
    {
        // Processing states
        [Description("Бланк")]
        Init = 1,
        
        [Description("В обробці")]
        Processing = 2,
        
        [Description("На погодженні")]
        Collision = 3,
        
        [Description("Погодженно")]
        CollisionApproved = 4,
        
        [Description("Відмовлено у погодженні")]
        CollisionDisapproved = 5,
        
        [Description("Очікування")]
        Waiting = 6,

        // Finished states
        [Description("Виконано")]
        Processed = 10,
        
        [Description("Відмовлено")]
        Rejected = 11,
        
        [Description("Скасовано")]
        Canceled = 12,
        
        [Description("Часткове завантаження")]
        PartLoad = 13,
        
        [Description("Часткове розвантаження")]
        PartUnload = 14,
        
        [Description("Перезавантаження")]
        Reload = 15
    }

    public enum ScaleOpDataType
    {
        Tare = 1,
        Gross = 2
    }
}