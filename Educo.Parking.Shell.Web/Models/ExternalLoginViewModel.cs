using System.ComponentModel.DataAnnotations;

namespace Educo.Parking.Shell.Web.Models
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "Введите e-mail пользователя")]
        [EmailAddress(ErrorMessage = "Неверный формат")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string returnUrl { get; set; }
    }
}
