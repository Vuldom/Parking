using System.ComponentModel.DataAnnotations;

namespace Educo.Parking.Shell.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите e-mail пользователя")]
        [EmailAddress(ErrorMessage = "Некорректный e-mail")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль пользователя")]
        [MinLength(3, ErrorMessage = "Минимальный размер пароля не менее 3 символов")]
        [StringLength(50, ErrorMessage = "Максимальная длина пароля - 50 символов")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
