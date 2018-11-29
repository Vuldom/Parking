using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите логин пользователя")]
        [StringLength(50, ErrorMessage = "Максимальная длина имени пользователя - 50 символов")]
        [Display(Name = "Логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите e-mail пользователя")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Неверный формат e-mail")]
        [Display(Name ="E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль пользователя")]
        [MinLength(8, ErrorMessage = "Минимальный размер пароля не менее 8 символов")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).*$", ErrorMessage = "Пароль должен состоять из букв нижнего и верхнего регистра, цифр и спец.символа")]
        [StringLength(50, ErrorMessage = "Максимальная длина пароля - 50 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль пользователя")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]        
        [Display(Name ="Confirm password")]
        [DataType(DataType.Password)]
        public string PasswordRepeat { get; set; }
    }
}
