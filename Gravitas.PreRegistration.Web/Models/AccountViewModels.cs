using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.PreRegistration.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [Display(Name = "Емайл")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Зберегти дані?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [Display(Name = "Емейл")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [Display(Name = "Емейл")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Зберегти дані?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [EmailAddress]
        [Display(Name = "Емайл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [StringLength(100, ErrorMessage = "{0} повинен містити щонайменше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [Display(Name = "Партнер")]
        public string PartnerId { get; set; }
        public string PartnerName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [EmailAddress]
        [Display(Name = "Емейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [StringLength(100, ErrorMessage = "{0} повинен містити щонайменше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Поле є обов'язковим для введення")]
        [EmailAddress]
        [Display(Name = "Емейл")]
        public string Email { get; set; }
    }
}
