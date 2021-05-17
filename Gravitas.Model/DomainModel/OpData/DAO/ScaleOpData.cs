using System;
using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class ScaleOpData : BaseOpData
    {
        public int TypeId { get; set; }
        public DateTime? TruckWeightDateTime { get; set; }
        public double? TruckWeightValue { get; set; }
        public bool? TruckWeightIsAccepted { get; set; }
        public bool TrailerIsAvailable { get; set; }
        public bool GuardianPresence { get; set; }
        public DateTime? TrailerWeightDateTime { get; set; }
        public double? TrailerWeightValue { get; set; }
        public bool? TrailerWeightIsAccepted { get; set; }
    }
}