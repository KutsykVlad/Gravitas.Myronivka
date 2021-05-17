using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class OpDataState : BaseEntity<int>
    {
        public OpDataState()
        {
            SingleWindowOpDataSet = new HashSet<SingleWindowOpData>();
            SecurityCheckInOpDataSet = new HashSet<SecurityCheckInOpData>();
            SecurityCheckOutOpDataSet = new HashSet<SecurityCheckOutOpData>();
            SecurityCheckReviewOpDataSet = new HashSet<SecurityCheckReviewOpData>();
            ScaleOpDataSet = new HashSet<ScaleOpData>();
            LabFacelessOpDataSet = new HashSet<LabFacelessOpData>();
            UnloadPointOpDataSet = new HashSet<UnloadPointOpData>();
            UnloadGuideOpDataSet = new HashSet<UnloadGuideOpData>();
            LoadPointOpDataSet = new HashSet<LoadPointOpData>();
            LoadGuideOpDataSet = new HashSet<LoadGuideOpData>();
            NonStandartOpDataSet = new HashSet<NonStandartOpData>();
            CentralLabOpDataSet = new HashSet<CentralLabOpData>();
            MixedFeedGuideOpDataSet = new HashSet<MixedFeedGuideOpData>();
            MixedFeedLoadOpDataSet = new HashSet<MixedFeedLoadOpData>();
        }

        public string Name { get; set; }

        // Navigation Properties
        public virtual ICollection<SingleWindowOpData> SingleWindowOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckInOpData> SecurityCheckInOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckOutOpData> SecurityCheckOutOpDataSet { get; set; }
        public virtual ICollection<SecurityCheckReviewOpData> SecurityCheckReviewOpDataSet { get; set; }
        public virtual ICollection<ScaleOpData> ScaleOpDataSet { get; set; }
        public virtual ICollection<LabFacelessOpData> LabFacelessOpDataSet { get; set; }
        public virtual ICollection<CentralLabOpData> CentralLabOpDataSet { get; set; }
        public virtual ICollection<UnloadPointOpData> UnloadPointOpDataSet { get; set; }
        public virtual ICollection<UnloadGuideOpData> UnloadGuideOpDataSet { get; set; }
        public virtual ICollection<LoadPointOpData> LoadPointOpDataSet { get; set; }
        public virtual ICollection<LoadGuideOpData> LoadGuideOpDataSet { get; set; }
        public virtual ICollection<NonStandartOpData> NonStandartOpDataSet { get; set; }
        public virtual ICollection<MixedFeedLoadOpData> MixedFeedLoadOpDataSet { get; set; }
        public virtual ICollection<MixedFeedGuideOpData> MixedFeedGuideOpDataSet { get; set; }
    }
}