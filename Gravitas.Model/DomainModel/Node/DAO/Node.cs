using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Traffic.DAO;
using Gravitas.Model.DomainValue;
using ScaleOpData = Gravitas.Model.DomainModel.OpData.DAO.ScaleOpData;
using SingleWindowOpData = Gravitas.Model.DomainModel.OpData.DAO.SingleWindowOpData;

namespace Gravitas.Model.DomainModel.Node.DAO
{
    public class Node
    {
        public Node()
        {
            LabFacelessOpDataSet = new HashSet<LabFacelessOpData>();
            SecurityCheckInOpDataSet = new HashSet<SecurityCheckInOpData>();
            SecurityCheckOutOpDataSet = new HashSet<SecurityCheckOutOpData>();
            SecurityCheckReviewOpDataSet = new HashSet<SecurityCheckReviewOpData>();
            ScaleOpDataSet = new HashSet<ScaleOpData>();
            UnloadPointOpDataSet = new HashSet<UnloadPointOpData>();
            UnloadGuideOpDataSet = new HashSet<UnloadGuideOpData>();
            LoadPointOpDataSet = new HashSet<LoadPointOpData>();
            LoadGuideOpDataSet = new HashSet<LoadGuideOpData>();
            LoadPointSet = new HashSet<LoadGuideOpData>();
            NonStandartOpDataSet = new HashSet<NonStandartOpData>();
            TrafficHistory = new HashSet<TrafficHistory>();
            Assignments = new HashSet<RoleAssignment>();
            CentralLabOpSet = new HashSet<CentralLabOpData>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int OrganizationUnitId { get; set; }
        public int OpRoutineId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmergency { get; set; }
        public int Quota { get; set; }
        public string Config { get; set; }
        public string Context { get; set; }
        public int MaximumProcessingTime { get; set; }
        public int MaximumDepartureTime { get; set; }

        public NodeGroup NodeGroup { get; set; }

        public virtual ICollection<LabFacelessOpData> LabFacelessOpDataSet { get; set; }
        public virtual ICollection<CentralLabOpData> CentralLabOpSet { get; set; }
        public virtual ICollection<SingleWindowOpData> SingleWindowOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckInOpData> SecurityCheckInOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckOutOpData> SecurityCheckOutOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckReviewOpData> SecurityCheckReviewOpDataSet { get; set; }
        public virtual ICollection<ScaleOpData> ScaleOpDataSet { get; set; }
        public virtual ICollection<UnloadPointOpData> UnloadPointOpDataSet { get; set; }
        public virtual ICollection<UnloadGuideOpData> UnloadGuideOpDataSet { get; set; }
        public virtual ICollection<LoadPointOpData> LoadPointOpDataSet { get; set; }
        public virtual ICollection<LoadGuideOpData> LoadGuideOpDataSet { get; set; }
        public virtual ICollection<LoadGuideOpData> LoadPointSet { get; set; }
        public virtual ICollection<NonStandartOpData> NonStandartOpDataSet { get; set; }
        public virtual ICollection<TrafficHistory> TrafficHistory { get; set; }
        public virtual ICollection<RoleAssignment> Assignments { get; set; }
    }
}