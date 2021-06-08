using System.ComponentModel;

namespace Gravitas.Model.DomainValue
{
    public enum OwnTransportType
    {
        None = 0,
        
        [Description("Власний")]
        Own = 1,
        
        [Description("Технологічний")]
        Tech = 2,
        
        [Description("Переміщення")]
        Move = 3
    }
}