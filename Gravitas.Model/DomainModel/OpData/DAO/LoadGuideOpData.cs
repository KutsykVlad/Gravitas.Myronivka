using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class LoadGuideOpData : BaseOpData
    {
        public int LoadPointNodeId { get; set; }
        public virtual Node.DAO.Node LoadPointNode { get; set; }
    }
}