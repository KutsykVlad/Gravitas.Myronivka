using System.ComponentModel.DataAnnotations;

namespace Gravitas.PreRegistration.Api.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
 
        [Required]
        [StringLength(100, ErrorMessage = "Довжина паролю повинна бути не менше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
 
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароль та підтвердження пвролю не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}