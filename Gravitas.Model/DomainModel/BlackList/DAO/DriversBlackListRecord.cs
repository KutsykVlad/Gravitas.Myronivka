using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.BlackList.DAO
{
    public class DriversBlackListRecord : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
