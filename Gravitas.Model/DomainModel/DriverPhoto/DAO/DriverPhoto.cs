using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.DriverPhoto.DAO
{
    public class DriverPhoto : BaseEntity<int>
    {
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }

        public int DeviceId { get; set; }
        public virtual Device.DAO.Device Device { get; set; }
    }
}