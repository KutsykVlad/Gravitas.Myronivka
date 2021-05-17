using System.ComponentModel.DataAnnotations;

namespace Gravitas.PreRegistration.Api.Models
{
    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Довжина паролю повинна бути не менше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Пароль та підтвердження паролю не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}