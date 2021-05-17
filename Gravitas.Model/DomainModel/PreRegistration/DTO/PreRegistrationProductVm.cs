using System.ComponentModel;

namespace Gravitas.Model.DomainModel.PreRegistration.DTO
{
    public class PreRegistrationProductVm
    {
        public long Id { get; set; }
        
        [DisplayName("Назва")]
        public string Title { get; set; }

        [DisplayName("Маршрут")]
        public string Route { get; set; }

        [DisplayName("Тривалість(хв)")]
        public int RouteTimeInMinutes { get; set; }
    }
}