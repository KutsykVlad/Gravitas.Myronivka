using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpRoutine.DAO
{
    public class OpRoutine : BaseEntity<int>
    {
        public OpRoutine()
        {
            OpRoutineStateSet = new HashSet<OpRoutineState>();
            NodeSet = new HashSet<Node.DAO.Node>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OpRoutineState> OpRoutineStateSet { get; set; }
        public virtual ICollection<Node.DAO.Node> NodeSet { get; set; }
    }
}