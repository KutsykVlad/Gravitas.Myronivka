using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Message.DAO
{
    public class TelegramBot : BaseEntity<int>
    {
        public string Name { get; set; } 
        public TelegramGroupType Type { get; set; } 
        public string Token { get; set; }
        public string ChatId { get; set; }
    }
}