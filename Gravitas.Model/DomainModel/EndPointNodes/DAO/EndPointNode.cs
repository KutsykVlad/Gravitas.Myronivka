using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.EndPointNodes.DAO
{
    public class EndPointNode : BaseEntity<int>
    {
        public int NodeId { get; set; }
        public virtual Node.DAO.Node Node { get; set; }
    }
}
