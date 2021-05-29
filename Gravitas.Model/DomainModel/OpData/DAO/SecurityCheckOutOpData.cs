using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class SecurityCheckOutOpData : BaseOpData
    {
        public bool? IsCameraImagesConfirmed { get; set; }
        
        public string SealList { get; set; }
    }
}