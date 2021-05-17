using System;
using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class CentralLabOpData : BaseOpData
    {
        public DateTime? SampleCheckInDateTime { get; set; }
        public DateTime? SampleCheckOutTime { get; set; }
        public string LaboratoryComment { get; set; }
        public string CollisionComment { get; set; }
    }
}
