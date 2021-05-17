using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel.Admin
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введіть логін")]
        [DisplayName("Логін")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Введіть пароль")]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}