using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.PreRegistration.Company
{
    public class PreRegisterCompanyVm
    {
        [DisplayName("Електронна адреса")]
        public string Email { get; set; }

        [DisplayName("Дозволений в'їзд")]
        public bool AllowToAdd { get; set; }

        [DisplayName("Максимальна кількість вантажівок")]
        public int TrucksMax { get; set; }

        [DisplayName("Зареєстрованих вантажівок")]
        public int TrucksInProgress { get; set; }

        [DisplayName("Компанія")]
        public string CompanyName { get; set; }

        [DisplayName("Код ЄДРПОУ або ФОП")]
        public string EnterpriseCode { get; set; }

        [DisplayName("Контактний телефон")]
        public string ContactPhoneNo { get; set; }
    }
}