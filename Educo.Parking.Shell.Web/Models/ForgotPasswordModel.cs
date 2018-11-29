using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [RegularExpression("[^А-ЯЁа-яё]*$", ErrorMessage = "Имя не должно содержать русских букв")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть от 3 до 50 символов", MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите email пользователя")]
        [EmailAddress(ErrorMessage = "Не верно введен email пользователя")]
        public string Email { get; set; }
    }
}
