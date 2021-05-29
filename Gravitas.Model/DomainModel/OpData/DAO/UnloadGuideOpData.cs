using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class UnloadGuideOpData : BaseOpData
    {
        public int UnloadPointNodeId { get; set; }
        public virtual Node.DAO.Node UnloadPointNode { get; set; }
    }
}