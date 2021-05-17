using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gravitas.PreRegistration.Web.Models.Administrative
{
    public class UserInfoVm
    {
        public string Id { get; set; }
        [DisplayName("Емейл")]
        public string Email { get; set; }
        [DisplayName("Ім'я партнера")]
        public string PartnerName { get; set; }
        [DisplayName("Дозвіл на реєстрацію")]
        public bool IsAllowedToRegister { get; set; }
    }
}