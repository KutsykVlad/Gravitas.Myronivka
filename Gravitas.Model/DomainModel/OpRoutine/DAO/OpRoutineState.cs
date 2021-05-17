using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpRoutine.DAO
{
    public class OpRoutineState : BaseEntity<int>
    {
        public OpRoutineState()
        {
            OpVisaSet = new HashSet<OpVisa.DAO.OpVisa>();
        }

        public int OpRoutineId { get; set; }
        public int StateNo { get; set; }
        public string Name { get; set; }

        public virtual OpRoutine OpRoutine { get; set; }
        public virtual ICollection<OpVisa.DAO.OpVisa> OpVisaSet { get; set; }
    }
}